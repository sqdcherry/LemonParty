using UnityEngine;

public class RewardPref : MonoBehaviour
{ 
    [SerializeField] private GameObject crossSprite;
    [SerializeField] private int rewardCount;
    
    public int GetRewardCount()
    {
        return rewardCount;
    }

    public void SetCross(bool crossActive, bool buttonActive)
    {
        crossSprite.SetActive(true);
    }
}