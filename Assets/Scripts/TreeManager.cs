using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    [SerializeField] private BuyTreePanel panel;
    [SerializeField] private List<GameObject> lemonsList;
    [SerializeField] private List<GameObject> activeTreesList;

    private bool isReadyToCollect = true;
    private LemonTree _currentLimonTree;
    private int _currentPrice;

    public static Action onBuyWorker;
    public static Action onBuyCar;
    public static Action onUpgrade;

    private int pasiveLemons
    {
        get => PlayerPrefs.GetInt("SpawnRate", 0);
        set => PlayerPrefs.SetInt("SpawnRate", value);
    }


    private void Start()
    {
        pasiveLemons = 1;
        // set trees for upgrade
        // set trees upgare level
        for (int i = 0; i < lemonsList.Count; i++)
        {
            if (lemonsList[i].GetComponent<LemonTree>().GetCurrentStage() == 3)
                activeTreesList.Add(lemonsList[i]);
        }
    }

    private IEnumerator PasiveCollect()
    {
        if (isReadyToCollect)
        {
            isReadyToCollect = false;
            UIManager.instance.UpdateLemonsCountText(pasiveLemons);
            yield return new WaitForSeconds(1f);
        }
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
        if (UIManager.instance.GetLemonsCount() - 1 >= 0)
        {
            for (int i = 0; i < activeTreesList.Count; i++)
            {
                if (activeTreesList[i].GetComponent<LemonTree>().GetUpgradeState() < 2)
                {
                    activeTreesList[i].GetComponent<LemonTree>().SetUpgradeState(1);
                    return;
                }
            }
        }
        else
            Debug.Log("Pop-up");
    }

    private void OnEnable()
    {
        onBuyWorker += BuyWorker;
        onBuyCar += BuyCar;
        onBuyCar += UpgradeTrees;
    }

    private void OnDisable()
    {
        onBuyWorker -= BuyWorker;
        onBuyCar -= BuyCar;
        onBuyCar -= UpgradeTrees;
    }
}