using UnityEngine;

public class RewardPref : MonoBehaviour
{ 
    [SerializeField] private GameObject crossSprite;
    [SerializeField] private GameObject lockSprite;
    [SerializeField] private int rewardCount;
    
    public int GetRewardCount()
    {
        return rewardCount;
    }

    public void SetCross()
    {
        crossSprite.SetActive(true);
    }

    public void SetLock(bool value)
    {
        lockSprite.SetActive(value);
    }
}