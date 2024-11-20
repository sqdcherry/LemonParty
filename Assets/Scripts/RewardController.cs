using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardController : MonoBehaviour
{
    [SerializeField] private Button claimButton;
    [Space(18)]
    [SerializeField] private RewardPref rewardPref;

    [Space(18)]
    [SerializeField] private List<Reward> rewards;

    [SerializeField] private Transform rewardGrid;

    private List<RewardPref> rewardPrefabs;

    private bool canClaimRewad;
    private float claimCooldown = 24f;
    private float claimDeadTime = 48f;

    private int currentStreak
    {
        get => PlayerPrefs.GetInt("CurrentStreak", 0);
        set => PlayerPrefs.SetInt("CurrentStreak", value);
    }

    private DateTime? lastClaimTime
    {
        get
        {
            string data = PlayerPrefs.GetString("LastClaimedTime", null);

            if (!string.IsNullOrEmpty(data))
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString("LastClaimedTime", value.ToString());
            else
                PlayerPrefs.DeleteKey("LastClaimedTime");
        }
    }

    private void Start()
    {
        InitPrefabs();
        StartCoroutine(RewardStateUpdater());
    }

    private void InitPrefabs()
    {
        rewardPrefabs = new List<RewardPref>();

        for (int i = 0; i < 18; i++)
        {
            rewardPrefabs.Add(Instantiate(rewardPref, rewardGrid, false));
            rewardPrefabs[i].SetRewarData(i, rewards[i].Value, rewards[i]);
        }

        for (int i = 0; i < currentStreak; i++)
            rewardPrefabs[i].SetCross(true, false);

        claimButton.transform.position = rewardPrefabs[currentStreak].transform.position;
    }

    private IEnumerator RewardStateUpdater()
    {
        while (true)
        {
            UpdateRewardsState();
            yield return new WaitForSeconds(1f);
        }    
    }

    private void UpdateRewardsState()
    {
        canClaimRewad = true;

        if (lastClaimTime.HasValue)
        {
            var timeSpan = DateTime.UtcNow - lastClaimTime.Value;

            if (timeSpan.TotalHours > claimDeadTime)
            {
                lastClaimTime = null;
                currentStreak = 0;
            }
            else if (timeSpan.TotalHours < claimCooldown)
                canClaimRewad = false;
        }

        UpdaterewardUI();
    }

    private void UpdaterewardUI()
    {
        claimButton.interactable = canClaimRewad;
        rewardPrefabs[currentStreak].SetInreactable(canClaimRewad);
        claimButton.transform.position = rewardPrefabs[currentStreak].transform.position;
    }

    public void ClaimReward()
    {
        if (!canClaimRewad)
            return;

        var reward = rewards[currentStreak];
        rewardPrefabs[currentStreak].SetCross(true, false);

        switch (reward.type)
        {
            case Reward.RewardType.COIN:
                PlayerJSON._instance.SaveBoughtCoinsFile(reward.Value);
                break;
            case Reward.RewardType.CRYSTAL:
                PlayerJSON._instance.SaveBoughtCrystalsFile(reward.Value);
                break;
        }

        lastClaimTime = DateTime.UtcNow;

        UpdateRewardsState();
        currentStreak = (currentStreak + 1) % 18;

        if (PlayerPrefs.HasKey("Notification"))
        {
            if (PlayerPrefs.GetInt("Notification") == 1)
                NotificationManager.instance.SendDailyRewardsNotification();
        }
    }
}