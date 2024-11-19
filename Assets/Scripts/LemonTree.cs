using System.Collections;
using UnityEngine;

public class LemonTree : MonoBehaviour
{
    [SerializeField] private GameObject _lock;
    [SerializeField] private GameObject _plot;
    [SerializeField] private GameObject _digUpPlot;
    [SerializeField] private GameObject _tree;

    private GameObject currentLemonsCount;
    [SerializeField] private GameObject noneCountLimons;
    [SerializeField] private GameObject lowCountLimons;
    [SerializeField] private GameObject midCountLimons;
    [SerializeField] private GameObject hightCountLimons;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private int index;

    private int collectableStage;
    private bool isReadyToSpawn = true;
    private bool isBought;

    private void Awake()
    {
        //PlayerPrefs.DeleteKey($"PlotState{index}");
        PlayerPrefs.SetInt($"PlotState{0}", 3);

        if (PlayerPrefs.HasKey($"UpgradeState{index}"))
        {
            if (PlayerPrefs.GetInt($"UpgradeState{index}") == 0)
            {
                currentLemonsCount = lowCountLimons;
            }
            else if (PlayerPrefs.GetInt("UpgradeState") == 1)
            {
                currentLemonsCount = midCountLimons;
            }
            else if (PlayerPrefs.GetInt("UpgradeState") == 2)
            {
                currentLemonsCount = midCountLimons;
            }
        }
        else
        {
            PlayerPrefs.SetInt("UpgradeState", 0);
            currentLemonsCount = lowCountLimons;
        }

        CheakState();
    }

    private int rewardPerClick
    {
        get => PlayerPrefs.GetInt($"CurrentRewardPerClick{index}", 0);
        set => PlayerPrefs.SetInt($"CurrentRewardPerClick{index}", value);
    }

    private void Start()
    {
        if (rewardPerClick == 0)
        {
            rewardPerClick = 1;
        }
        //PlayerPrefs.SetInt($"CurrentRewardPerClick{0}", 5);

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
        if (isReadyToSpawn && collectableStage < 1)
        {
            isReadyToSpawn = false;
            yield return new WaitForSeconds(0.1f);
            collectableStage++;
            currentLemonsCount.SetActive(false);
            yield return new WaitForSeconds(3f);
            currentLemonsCount.SetActive(true);
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
    //private void CheakIsBought()
    //{
    //    if (PlayerPrefs.HasKey($"PlotIsBought{index}"))
    //    {
    //        if (PlayerPrefs.GetInt($"PlotIsBought{index}") == 1)
    //        {
    //            isBought = true;
    //            activeTree.SetActive(true);
    //            nonActiveTree.SetActive(false);
    //        }
    //        else
    //        {
    //            isBought = false;
    //            activeTree.SetActive(false);
    //            nonActiveTree.SetActive(true);
    //        }
    //    }
    //    else
    //    {
    //        isBought = false;
    //        activeTree.SetActive(false);
    //        nonActiveTree.SetActive(true);
    //        PlayerPrefs.SetInt($"PlotIsBought{index}", 0);
    //    }
    //}

    private void UpdateState()
    {
        if (PlayerPrefs.HasKey($"UpgradeState{index}"))
        {
            if (PlayerPrefs.GetInt($"UpgradeState{index}") == 0)
            {
                lowCountLimons.SetActive(true);
                midCountLimons.SetActive(false);
                hightCountLimons.SetActive(false);
                noneCountLimons.SetActive(false);
            }
            else if (PlayerPrefs.GetInt($"UpgradeState{index}") == 1)
            {
                lowCountLimons.SetActive(false);
                midCountLimons.SetActive(true);
                hightCountLimons.SetActive(false);
                noneCountLimons.SetActive(false);
            }
            else if (PlayerPrefs.GetInt($"UpgradeState{index}") == 2)
            {
                lowCountLimons.SetActive(false);
                midCountLimons.SetActive(false);
                hightCountLimons.SetActive(true);
                noneCountLimons.SetActive(false);
            }
            else
            {
                lowCountLimons.SetActive(false);
                midCountLimons.SetActive(false);
                hightCountLimons.SetActive(false);
                noneCountLimons.SetActive(true);
            }        
        }
    }

    public int GetCurrentStage()
    {
        int currentState = PlayerPrefs.GetInt($"PlotState{index}");

        if (currentState == 3)
        {
            return 3;
        }
        else if (currentState == 2)
        {
            return 2;
        }
        else if (currentState == 1)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public bool GetIsBought()
    {
        if (isBought)
            return true;
        else
            return false;
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

    public void Collected()
    {
        if (collectableStage > 0)
        {
            collectableStage--;
            UpdateState();
            Debug.Log(gameObject);
            UIManager.instance.UpdateLemonsCountText(rewardPerClick);
        }
    }
}