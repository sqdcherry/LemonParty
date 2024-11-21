using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private TMP_Text lemonsCountText;
    [SerializeField] private GameObject workersPanel;
    [SerializeField] private GameObject ugradesPanel;

    private int _lemonsCount
    {
        get => PlayerPrefs.GetInt("LemonsCount", 0);
        set => PlayerPrefs.SetInt("LemonsCount", value);
    }

    private void Start()
    {
        //_lemonsCount = 0;
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        lemonsCountText.text = _lemonsCount.ToString();
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