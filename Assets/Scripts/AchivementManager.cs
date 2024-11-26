using UnityEngine;
using UnityEngine.UI;

public class AchivementManager : MonoBehaviour
{
    [SerializeField] private Button[] achivementButton;

    private void Start()
    {
        if (!PlayerPrefs.HasKey($"AcivementIsGot{1000}"))
        {
            if (PlayerPrefs.HasKey("BoughtWorkers"))
            {
                if (PlayerPrefs.GetInt("BoughtWorkers") > 0)
                {
                    achivementButton[0].interactable = true;
                }
                else
                    achivementButton[0].interactable = false;
            }
        }
        if (!PlayerPrefs.HasKey($"AcivementIsGot{3000}"))
        {
            if (UIManager.instance.GetLemonsCount() >= 5000)
            {
                if (PlayerPrefs.GetInt("BoughtWorkers") > 0)
                {
                    achivementButton[0].interactable = true;
                }
                else
                    achivementButton[0].interactable = false;
            }
        }
        if (!PlayerPrefs.HasKey($"AcivementIsGot{10000}"))
        {
            if (PlayerPrefs.HasKey("BoughtCars"))
            {
                if (PlayerPrefs.GetInt("BoughtWorkers") > 0)
                {
                    achivementButton[0].interactable = true;
                }
                else
                    achivementButton[0].interactable = false;
            }
            }
        }

    public void OnGetAchivementReward(int reward)
    {
        UIManager.instance.UpdateLemonsCountText(reward);
        achivementButton[0].interactable = false;
        PlayerPrefs.SetInt($"AcivementIsGot{reward}", 1);
    }
}