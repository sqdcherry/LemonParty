using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RewardPref : MonoBehaviour
{
    [SerializeField] private TMP_Text rewardText;
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private Button rewardButton;
    [SerializeField] private Image rewardIcon;
    [SerializeField] private GameObject crossSprite;
    [SerializeField] private Sprite rewardCoin;
    [SerializeField] private Sprite rewardCrystal;

    //public void SetRewarData(int day, int rewardsText, Reward reward)
    //{
    //    if (PlayerPrefs.HasKey("Language"))
    //    {
    //        if (PlayerPrefs.GetInt("Language") == 0)
    //            dayText.text = $"Day {day + 1}";
    //        else if (PlayerPrefs.GetInt("Language") == 1)
    //            dayText.text = $"Día {day + 1}";
    //        else
    //            dayText.text = $"Tag {day + 1}";
    //    }
    //    else
    //        dayText.text = $"Day {day + 1}";
    //    rewardIcon.sprite = reward.type == Reward.RewardType.COIN ? rewardCoin : rewardCrystal;
    //    rewardText.text = rewardsText.ToString();
    //}

    public void SetInreactable(bool value)
    {
        rewardButton.interactable = value;
    }

    public void SetCross(bool crossActive, bool buttonActive)
    {
        crossSprite.SetActive(true);
        //rewardButton.interactable = false;
    }
}