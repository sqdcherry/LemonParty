using System.Collections;
using UnityEngine;

public class LemonTree : MonoBehaviour
{
    [SerializeField] private GameObject _lock;
    [SerializeField] private GameObject _plot;
    [SerializeField] private GameObject _digUpPlot;
    [SerializeField] private GameObject _tree;

    [SerializeField] private Sprite lowCountLimons;
    [SerializeField] private Sprite midCountLimons;
    [SerializeField] private Sprite hightCountLimons;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private int index;

    private int collectableStage;
    private bool isReadyToSpawn = true;
    private bool isBought;

    private void Awake()
    {
        PlayerPrefs.SetInt($"PlotState{0}", 3);
        Debug.Log(PlayerPrefs.GetInt($"PlotState{index}"));

        CheakState();
    }

    private void Start()
    {
        collectableStage = 1;
    }

    private void Update()
    {
        StartCoroutine(SpawnLimons());
    }

    private IEnumerator SpawnLimons()
    {
        if (isReadyToSpawn && collectableStage < 3)
        {
            isReadyToSpawn = false;
            collectableStage++;
            UpdateState();
            yield return new WaitForSeconds(3);
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
        if (spriteRenderer)
        {
            switch (collectableStage)
            {
                case 1:
                    spriteRenderer.sprite = lowCountLimons;
                    break;
                case 2:
                    spriteRenderer.sprite = midCountLimons;
                    break;
                case 3:
                    spriteRenderer.sprite = hightCountLimons;
                    break;
                case 0:
                    spriteRenderer.sprite = lowCountLimons;
                    break;
            }
        }
    }

    public bool GetCollectableStage()
    {
        int currentState = PlayerPrefs.GetInt($"PlotState{index}");

        if (currentState == 3)
        {
            return true;
        }
        else if (currentState == 2)
        {
            return false;
        }
        else if (currentState == 1)
        {
            return false;
        }
        else
        {
            return false;
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
            case 3:
                {
                    // upgrade
                    Debug.Log("upgrade");
                }
                break;

        }
        
        CheakState();
    }

    public void Collected()
    {
        if (collectableStage > 0)
        {
            collectableStage--;
            Debug.Log(collectableStage);
            UpdateState();
            UIManager.instance.UpdateLemonsCountText(1);
        }
    }
}