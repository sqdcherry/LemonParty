using System.Collections;
using UnityEngine;

public class LemonTree : MonoBehaviour
{
    [SerializeField] private GameObject _lock;
    [SerializeField] private GameObject _plot;
    [SerializeField] private GameObject _digUpPlot;
    [SerializeField] private GameObject _tree;

    [SerializeField] private GameObject noneCountLimons;
    [SerializeField] private GameObject lowCountLimons;
    [SerializeField] private GameObject midCountLimons;
    [SerializeField] private GameObject hightCountLimons;
    [SerializeField] private GameObject greenHouse;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private int index;

    private GameObject currentLemonsCount;

    private int collectableStage;
    private bool isReadyToSpawn = true;
    private bool isReadyToCollect;
    private int i;

    private int rewardPerClick
    {
        get => PlayerPrefs.GetInt($"CurrentRewardPerClick{index}", 0);
        set => PlayerPrefs.SetInt($"CurrentRewardPerClick{index}", value);
    }
    
    private int upgradeState2X
    {
        get => PlayerPrefs.GetInt($"Upgrade2X{index}", 0);
        set => PlayerPrefs.SetInt($"Upgrade2X{index}", value);
    }

    private int upgradeState4X
    {
        get => PlayerPrefs.GetInt($"Upgrade4X{index}", 0);
        set => PlayerPrefs.SetInt($"Upgrade4X{index}", value);
    }

    private void Awake()
    {
        PlayerPrefs.SetInt($"PlotState{0}", 3);
        UpdateState();
        CheakState();
    }

    private void Start()
    {
        if (rewardPerClick == 0)
        {
            rewardPerClick = 10;
        }

        collectableStage = 0;
    }

    private void Update()
    {
        StartCoroutine(SpawnLimons());
    }

    public void SetValueRewardPerClick(int countPerClick)
    {
        rewardPerClick = countPerClick;
    }

    private IEnumerator SpawnLimons()
    {
        if (isReadyToSpawn && collectableStage == 0)
        {
            collectableStage++;
            isReadyToSpawn = false;
            isReadyToCollect = false;
            currentLemonsCount.SetActive(false);
            noneCountLimons.SetActive(true);
            yield return new WaitForSeconds(1f);
            currentLemonsCount.SetActive(true);
            noneCountLimons.SetActive(false);
            isReadyToCollect = true;
            isReadyToSpawn = true;
        }
    }

    private void CheakState()
    {
        if (PlayerPrefs.HasKey($"PlotState{index}"))
        {
            int currentState = PlayerPrefs.GetInt($"PlotState{index}");
            
            switch (currentState)
            {
                case 0:
                    {
                        _lock.SetActive(true);
                        _plot.SetActive(false);
                        _digUpPlot.SetActive(false);
                        _tree.SetActive(false);
                    
                    }
                    break;
                case 1:
                    {
                        _plot.SetActive(true);
                        _lock.SetActive(false);
                        _digUpPlot.SetActive(false);
                        _tree.SetActive(false);
                    }
                    break;
                case 2:
                    {
                        _lock.SetActive(false);
                        _plot.SetActive(false);
                        _digUpPlot.SetActive(true);
                        _tree.SetActive(false);
                    }
                    break;
                case 3:
                    {
                        _lock.SetActive(false);
                        _plot.SetActive(false);
                        _digUpPlot.SetActive(false);
                        _tree.SetActive(true);
                    }
                    break;
            }
        }
        else
        {
            _lock.SetActive(true);
            _plot.SetActive(false);
            _digUpPlot.SetActive(false);
            _tree.SetActive(false);
            PlayerPrefs.SetInt($"PlotState{index}", 0);
        }
    }


    private void UpdateState()
    {
        if (upgradeState2X == 1)
        {
            currentLemonsCount = hightCountLimons;
        }
        else if (upgradeState4X == 1)
        {
            currentLemonsCount = hightCountLimons;
            greenHouse.SetActive(true);
        }
        else
        {
            currentLemonsCount = lowCountLimons;
        }
    }

    public int GetCurrentStage()
    {
        int currentState = PlayerPrefs.GetInt($"PlotState{index}");

        return currentState;
    }

    public bool GetIsReadyToCollect()
    {
        return isReadyToCollect;
    }

    public void SetUpgrade(int value)
    {
        upgradeState2X = 1;
        currentLemonsCount.SetActive(false);
        currentLemonsCount = hightCountLimons;
        currentLemonsCount.SetActive(true);
        rewardPerClick *= value;
    }

    public void SetUpgradeGreenHouse(int value)
    {
        upgradeState4X = 1;
        rewardPerClick *= value;
        currentLemonsCount.SetActive(false);
        currentLemonsCount = hightCountLimons;
        currentLemonsCount.SetActive(true);
        greenHouse.SetActive(true);
    }

    public void BuyPlot()
    {
        int currentState = PlayerPrefs.GetInt($"PlotState{index}");

        switch (currentState)
        {
            case 0:
                {
                    _lock.SetActive(false);
                    _plot.SetActive(true);
                    _digUpPlot.SetActive(false);
                    _tree.SetActive(false);
                    PlayerPrefs.SetInt($"PlotState{index}", 1);
                }
                break;
            case 1:
                {
                    _lock.SetActive(false);
                    _plot.SetActive(false);
                    _digUpPlot.SetActive(true);
                    _tree.SetActive(false);
                    PlayerPrefs.SetInt($"PlotState{index}", 2);
                }
                break;
            case 2:
                {
                    _lock.SetActive(false);
                    _plot.SetActive(false);
                    _digUpPlot.SetActive(false);
                    _tree.SetActive(true);
                    PlayerPrefs.SetInt($"PlotState{index}", 3);
                }
                break;
        }
    }

    public int GetUpgrade2X()
    {
        if (PlayerPrefs.HasKey($"Upgrade2X{index}"))
            return upgradeState2X;
        else
            return 0;
    }

    public int GetUpgrade4X()
    {
        if (PlayerPrefs.HasKey($"Upgrade4X{index}"))
            return upgradeState4X;
        else
            return 0;
    }

    public void Collected()
    {
        if (collectableStage > 0)
        {
            collectableStage--;
            UIManager.instance.UpdateLemonsCountText(rewardPerClick);
        }
    }
}