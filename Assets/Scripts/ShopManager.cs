using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private TMP_Text workerText;
    [SerializeField] private TMP_Text carText;

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
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
        if (UIManager.instance.GetLemonsCount() - 500 >= 0)
        {
            TreeManager.onBuyWorker?.Invoke();
            if (PlayerPrefs.HasKey("BoughtWorkers"))
            {
                int value = PlayerPrefs.GetInt("BoughtWorkers");
                if (value < 6)
                    workerText.text = value.ToString();
            }
            else
                workerText.text = 1.ToString();
        }
        else
            PopUpManager.instance.StartPopUpAnimation("Not enoght lemons");
    }

    public void OnBuyCar()
    {
        if (UIManager.instance.GetLemonsCount() - 5000 >= 0)
        {
            TreeManager.onBuyCar?.Invoke();
            if (PlayerPrefs.HasKey("BoughtCars"))
            {
                int value = PlayerPrefs.GetInt("BoughtCars");
                if (value < 6)
                    carText.text = value.ToString();
            }
            else
                carText.text = 1.ToString();
        }
        else
            PopUpManager.instance.StartPopUpAnimation("Not enoght lemons");
    }

    public void OnBuyUpgarade2X()
    {
        if (UIManager.instance.GetLemonsCount() - 500 >= 0)
        {
            TreeManager.onUpgrade2X?.Invoke();
        }
        else
            PopUpManager.instance.StartPopUpAnimation("Not enoght lemons");
    }

    public void OnBuyUpgarade4X()
    {
        if (UIManager.instance.GetLemonsCount() - 2500 >= 0)
        {
            TreeManager.onUpgrade4X?.Invoke();
        }
        else
            PopUpManager.instance.StartPopUpAnimation("Not enoght lemons");
    }
}