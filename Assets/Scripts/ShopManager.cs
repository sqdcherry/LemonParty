using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private TMP_Text workerText;
    [SerializeField] private TMP_Text carText;

    private void Awake()
    {
        PlayerPrefs.DeleteKey("Upgrade2x");
        PlayerPrefs.DeleteKey("Upgrade4x");
        if (PlayerPrefs.HasKey("BoughtCars"))
        {
            carText.text = PlayerPrefs.GetInt("BoughtCars").ToString();
        }
        if (PlayerPrefs.HasKey("BoughtWorkers"))
        {
            workerText.text = PlayerPrefs.GetInt("BoughtWorkers").ToString();
        }
    }

    public void OnBuyWorker()
    {        
        //if (UIManager.instance.GetLemonsCount() - 100 >= 0)
        //{
        TreeManager.onBuyWorker?.Invoke();
        if (PlayerPrefs.HasKey("BoughtWorkers"))
        {
            int value = PlayerPrefs.GetInt("BoughtWorkers");
            if (value < 6)
                workerText.text = value.ToString();
        }
        else
            workerText.text = 1.ToString();
        //}
        //else pop or uninteractable buttons
    }

    public void OnBuyCar()
    {
        //if (UIManager.instance.GetLemonsCount() - 100 >= 0)
        //{
            TreeManager.onBuyCar?.Invoke();
        if (PlayerPrefs.HasKey("BoughtCars"))
        {
            int value = PlayerPrefs.GetInt("BoughtCars");
            if (value < 6)
                carText.text = value.ToString();
        }
        else
            carText.text = 1.ToString();
        //}
        //else pop or uninteractable buttons
    }

    public void OnBuyUpgarade2X()
    {
        if (!PlayerPrefs.HasKey("Upgrade2x"))
        {
            Debug.Log("!");
            PlayerPrefs.SetInt("Upgrade2x", PlayerPrefs.GetInt("Upgrade2x") + 1);
            TreeManager.onUpgrade2X?.Invoke();
        }
        else
            Debug.Log("popup");
    }

    public void OnBuyUpgarade4X()
    {
        if (!PlayerPrefs.HasKey("Upgrade4x"))
        {
            PlayerPrefs.SetInt("Upgrade4x", PlayerPrefs.GetInt("Upgrade4x") + 1);
            TreeManager.onUpgrade4X?.Invoke();
        }
        else
            Debug.Log("popup");
    }
}