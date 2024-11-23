using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardController : MonoBehaviour
{
    [SerializeField] private Button claimButton;
    [SerializeField] private GameObject bonusPanel;

    [SerializeField] private List<RewardPref> rewardPrefabs;

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
        for (int i = 0; i < currentStreak; i++)
            rewardPrefabs[i].SetCross(true, false);
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
        claimButton.transform.position = rewardPrefabs[currentStreak].transform.position;
    }

    public void ClaimReward()
    {
        if (!canClaimRewad)
            return;

        rewardPrefabs[currentStreak].SetCross(true, false);

        UIManager.instance.UpdateLemonsCountText(rewardPrefabs[currentStreak].GetRewardCount());
        bonusPanel.SetActive(true);
        bonusPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = rewardPrefabs[currentStreak].GetRewardCount().ToString();

        lastClaimTime = DateTime.UtcNow;

        UpdateRewardsState();
        currentStreak = (currentStreak + 1) % 7;
    }
}