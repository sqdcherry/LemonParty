using System;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    [SerializeField] private BuyTreePanel panel;
    [SerializeField] private List<GameObject> lemonsList;
    [SerializeField] private List<GameObject> activeTreesList;
    [SerializeField] private Worker worker;
    [SerializeField] private Car car;

    private LemonTree _currentLimonTree;
    private int _currentPrice;

    public static Action onBuyWorker;
    public static Action onBuyCar;
    public static Action onUpgrade2X;
    public static Action onUpgrade4X;

    private void Start()
    {
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
        foreach (GameObject element in activeTreesList)
        {
            if (element.GetComponent<LemonTree>().GetIsReadyToCollect())
            {
                element.GetComponent<LemonTree>().Collected();
                return;
            }
        } 
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
            _currentPrice = 100;
        }
        else if (stage == 1)
        {
            panel.gameObject.SetActive(true);
            panel.headText.text = "DIG UP A PLOT";
            panel.priceText.text = 300.ToString();
            _currentPrice = 300;
        }
        else if (stage == 2)
        {
            panel.gameObject.SetActive(true);
            panel.headText.text = "SOW A PLOT";
            panel.priceText.text = 500.ToString();
            _currentPrice = 500;
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
        else
            PopUpManager.instance.StartPopUpAnimation("Not enoght lemons");
    }

    public void BuyWorker()
    {
        if (PlayerPrefs.HasKey("BoughtWorkers"))
        {
            if (PlayerPrefs.GetInt("BoughtWorkers") < 5)
            {
                PlayerPrefs.SetInt("BoughtWorkers", PlayerPrefs.GetInt("BoughtWorkers") + 1);
                worker.gameObject.SetActive(true);
                worker.SetPasiveLemons(150);
            }
            else
            {
                PopUpManager.instance.StartPopUpAnimation("All workers was bought");
            }
        }
        else
        {
            PlayerPrefs.SetInt("BoughtWorkers", 1);
            worker.gameObject.SetActive(true);
            worker.SetPasiveLemons(150);
        }
    }

    public void BuyCar()
    {
        if (PlayerPrefs.HasKey("BoughtCars"))
        {
            if (PlayerPrefs.GetInt("BoughtCars") < 5)
            {
                PlayerPrefs.SetInt("BoughtCars", PlayerPrefs.GetInt("BoughtCars") + 1);
                car.gameObject.SetActive(true);
                car.SetPasiveLemons(1500);
            }
            else
            {
                PopUpManager.instance.StartPopUpAnimation("All cars was bought");
            }
        }
        else
        {
            PlayerPrefs.SetInt("BoughtCars", 1);
            car.gameObject.SetActive(true);
            car.SetPasiveLemons(1500);
        }
    }

    public void Upgrade2X()
    {
        List<LemonTree> trees = new List<LemonTree>();

        foreach (GameObject element in activeTreesList)
        {
            if (element.GetComponent<LemonTree>().GetUpgrade2X() != 1)
            {
                trees.Add(element.GetComponent<LemonTree>());
            }
        }

        if (trees.Count != 0)
            trees[0].SetUpgrade(2);
        else
            PopUpManager.instance.StartPopUpAnimation("All trees was improve");
    }

    public void Upgrade4X()
    {
        List<LemonTree> trees = new List<LemonTree>(); 

        foreach (GameObject element in activeTreesList)
        {
            if (element.GetComponent<LemonTree>().GetUpgrade4X() != 1)
            {
                trees.Add(element.GetComponent<LemonTree>());
            }
        }

        if (trees.Count != 0)
            trees[0].SetUpgradeGreenHouse(4);
        else
            PopUpManager.instance.StartPopUpAnimation("All trees was improve");
    }

    private void OnEnable()
    {
        onBuyWorker += BuyWorker;
        onBuyCar += BuyCar;
        onUpgrade2X += Upgrade2X;
        onUpgrade4X += Upgrade4X;
    }

    private void OnDisable()
    {
        onBuyWorker -= BuyWorker;
        onBuyCar -= BuyCar;
        onUpgrade2X -= Upgrade2X;
        onUpgrade4X -= Upgrade4X;
    }
}