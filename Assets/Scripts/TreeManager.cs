using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    [SerializeField] private BuyTreePanel panel;
    [SerializeField] private List<GameObject> lemonsList;
    [SerializeField] private List<GameObject> activeTreesList;
    [SerializeField] private Worker worker;
    [SerializeField] private GameObject car;

    private LemonTree _currentLimonTree;
    private int _currentPrice;

    public static Action onBuyWorker;
    public static Action onBuyCar;
    public static Action onUpgrade;

    private void Start()
    {
        PlayerPrefs.SetInt("BoughtWorkers", 0);
        if (PlayerPrefs.HasKey("BoughtWorkers"))
        {
            if (PlayerPrefs.GetInt("BoughtWorkers") > 0)
                worker.gameObject.SetActive(true);
        }
        else
            PlayerPrefs.SetInt("BoughtWorkers", 0);
        if (PlayerPrefs.HasKey("BoughtCars"))
        {
            if (PlayerPrefs.GetInt("BoughtCars") > 0)
                worker.gameObject.SetActive(true);
        }
        else
            PlayerPrefs.SetInt("BoughtCars", 0);

        for (int i = 0; i < lemonsList.Count; i++)
        {
            if (lemonsList[i].GetComponent<LemonTree>().GetCurrentStage() == 3)
                activeTreesList.Add(lemonsList[i]);
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
        if (PlayerPrefs.HasKey("BoughtWorkers"))
        {
            if (PlayerPrefs.GetInt("BoughtWorkers") < 5)
            {
                PlayerPrefs.SetInt("BoughtWorkers", PlayerPrefs.GetInt("BoughtWorkers") + 1);
                worker.gameObject.SetActive(true);
                worker.SetPasiveLemons(12);
            }
            else
            {
                Debug.Log("popup");
            }
        }
        else
        {
            PlayerPrefs.SetInt("BoughtWorkers", 1);
            worker.gameObject.SetActive(true);
            worker.SetPasiveLemons(12);
        }
    }

    public void BuyCar()
    {
        if (PlayerPrefs.HasKey("BoughtCars"))
        {
            if (PlayerPrefs.GetInt("BoughtCars") < 5)
            {
                PlayerPrefs.SetInt("BoughtCars", PlayerPrefs.GetInt("BoughtCars") + 1);
                worker.gameObject.SetActive(true);
                worker.SetPasiveLemons(12);
            }
            else
            {
                Debug.Log("popup");
            }
        }
        else
        {
            PlayerPrefs.SetInt("BoughtCars", 1);
            worker.gameObject.SetActive(true);
            worker.SetPasiveLemons(12);
        }
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