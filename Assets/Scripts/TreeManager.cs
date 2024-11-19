using System;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    [SerializeField] private BuyTreePanel panel;
    [SerializeField] private List<GameObject> lemonsList;
    [SerializeField] private List<GameObject> activeTreesList;

    private bool isReadyToSpawn = true;
    private LemonTree _currentLimonTree;
    private int _currentPrice;

    public static Action onBuyWorker;
    public static Action onBuyCar;

    private int spawnDelay
    {
        get => PlayerPrefs.GetInt("SpawnRate", 0);
        set => PlayerPrefs.SetInt("SpawnRate", value);
    }


    private void Start()
    {
        // set trees for upgrade
        // set trees upgare level
        for (int i = 0; i < lemonsList.Count; i++)
        {
            if (lemonsList[i].GetComponent<LemonTree>().GetCurrentStage() == 3)
                SetActiveTreesList(lemonsList[i]);
        }
    }

    public void SetActiveTreesList(GameObject boughtLemonTree)
    {
        activeTreesList.Add(boughtLemonTree);
    }

    public void Collect()
    {
        int currentColectItem = UnityEngine.Random.Range(0, activeTreesList.Count);
        activeTreesList[currentColectItem].GetComponent<LemonTree>().Collected();    
    }

    public void BuyPlotPanel(LemonTree currentLimonTree)
    {
        int stage = currentLimonTree.GetCurrentStage();
        _currentLimonTree = currentLimonTree;

        if (stage == 0)
        {
            panel.gameObject.SetActive(true);
            panel.headText.text = "BUY A PLOT";
            panel.priceText.text = 100.ToString();
            _currentPrice = 1;
        }
        else if (stage == 1)
        {
            panel.gameObject.SetActive(true);
            panel.headText.text = "DIG UP A PLOT";
            panel.priceText.text = 300.ToString();
            _currentPrice = 3;
        }
        else if (stage == 2)
        {
            panel.gameObject.SetActive(true);
            panel.headText.text = "SOW A PLOT";
            panel.priceText.text = 300.ToString();
            _currentPrice = 5;
        }
    }

    public void BuyPlot()
    {
        if (UIManager.instance.GetLemonsCount() - _currentPrice >= 0)
        {
            _currentLimonTree.BuyPlot();
            UIManager.instance.UpdateLemonsCountText(-_currentPrice);
            panel.gameObject.SetActive(false);

            int currentStage = _currentLimonTree.GetCurrentStage();
            if (currentStage == 3)
                activeTreesList.Add(_currentLimonTree.gameObject);
        }
    }

    public void BuyWorker()
    {
        // if lemons amount - price <= 0
        Debug.Log("WorkerIsBought");
        //+ pasive rewards
    }

    public void BuyCar()
    {
        // if lemons amount - price <= 0
        Debug.Log("CarIsBought");
        //+ pasive rewards
    }

    public void UpgradeTrees()
    {
        // if lemons amount - price <= 0 %% 
        GameObject currentLemonTree = null;

        for (int i = 0; i < activeTreesList.Count; i++)
        {
            if (activeTreesList[i].GetComponent<LemonTree>().GetUpgradeState() < 2)
            {
                activeTreesList[i].GetComponent<LemonTree>().UpgradeState(2);
                return;
            }
        }

        //currentLemonTree.GetComponent<LemonTree>().SetUpgradeState(1);
    }

    private void OnEnable()
    {
        onBuyWorker += BuyWorker;
        onBuyCar += BuyCar;
    }

    private void OnDisable()
    {
        onBuyWorker -= BuyWorker;
        onBuyCar -= BuyCar;
    }
}