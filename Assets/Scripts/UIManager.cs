using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private TMP_Text lemonsCountText;
    [SerializeField] private GameObject workersPanel;
    [SerializeField] private GameObject ugradesPanel;
    [SerializeField] private GameObject minigameButton;
    [SerializeField] private GameObject mainPanel;

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
            yield return new WaitForSeconds(180);
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
    }

    public void ExitPanel(GameObject panel)
    {
        panel.SetActive(false);
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
}