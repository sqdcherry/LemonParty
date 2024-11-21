using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;

    public void OnBuyWorker()
    {
        //if (UIManager.instance.GetLemonsCount() - 100 >= 0)
        //{
            TreeManager.onBuyWorker?.Invoke();
        //}
        //else pop or uninteractable buttons
    }

    public void OnBuyCar()
    {
        // if lemons amount - price <= 0
        if (PlayerPrefs.HasKey("CarIsBought"))
        {
            if (PlayerPrefs.GetInt("CarIsBought") < 5)
                PlayerPrefs.SetInt("CarIsBought", PlayerPrefs.GetInt("CarIsBought") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("CarIsBought", 1);
        }

        TreeManager.onBuyCar?.Invoke();
        //else pop or uninteractable buttons
    }

    public void OnBuyUpgarade()
    {
        TreeManager.onUpgrade?.Invoke();
    }
}