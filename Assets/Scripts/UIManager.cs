using System;
using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private TMP_Text lemonsCountText;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private GameObject workersPanel;
    [SerializeField] private GameObject ugradesPanel;
    [SerializeField] private GameObject minigameButton;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private float upPosPanel, midlePosPanel;

    private bool isReadyToStart = true;

    private int _lemonsCount
    {
        get => PlayerPrefs.GetInt("LemonsCount", 0);
        set => PlayerPrefs.SetInt("LemonsCount", value);
    }

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        if (PlayerPrefs.HasKey("FirstLoad"))
            startPanel.SetActive(false);
        else
            startPanel.SetActive(true);

        DateTime lastSaveTime = DateTimeController.GetDateTime("LastSaveTime", DateTime.UtcNow);
        TimeSpan timePassed = DateTime.UtcNow - lastSaveTime;
        int minutesPassed = (int)timePassed.TotalMinutes;

        int pasiveLemons = 0;
        pasiveLemons += minutesPassed * PlayerPrefs.GetInt("workerPasiveLemons");
        pasiveLemons += minutesPassed * PlayerPrefs.GetInt("carPasiveLemons");
        if (pasiveLemons > 15000)
        {
            _lemonsCount = 15000;
        }

        _lemonsCount = pasiveLemons;

        lemonsCountText.text = _lemonsCount.ToString();
    }

    private void Update()
    {
        StartCoroutine(StartMiniGame());
    }

    private IEnumerator StartMiniGame()
    {
        if (isReadyToStart)
        {
            isReadyToStart = false;
            yield return new WaitForSeconds(60);
            minigameButton.SetActive(true);
            isReadyToStart = true;
        }
    }

    public void UpdateLemonsCountText(int lemonsCount)
    {
        _lemonsCount += lemonsCount;
        lemonsCountText.text = _lemonsCount.ToString();
    }

    public int GetLemonsCount()
    {
        return _lemonsCount;
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        Leaderboard.instance.UploadEntries(PlayerPrefs.GetString("Name"), _lemonsCount);
        panel.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosY(midlePosPanel, 0.5f);
    }

    public async void ExitPanel(GameObject panel)
    {
        await PausePanelOutro(panel);
        panel.SetActive(false);
    }
    async Task PausePanelOutro(GameObject panel)
    {
        await panel.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosY(upPosPanel, 0.5f).AsyncWaitForCompletion();
    }

    public void OpenMiniGamePanel(GameObject panel)
    {
        panel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void SwapPanels()
    {
        if (workersPanel.activeSelf)
        {
            workersPanel.SetActive(false);
            ugradesPanel.SetActive(true);
        }
        else
        {
            workersPanel.SetActive(true);
            ugradesPanel.SetActive(false);
        }
    }

    public void OnConfirmName()
    {
        if (nameInputField.text != "")
        {
            PlayerPrefs.SetString("Name", nameInputField.text);
            PlayerPrefs.SetInt("FirstLoad", 1);
            startPanel.SetActive(false);
        }
    }
}