using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limon : MonoBehaviour
{
    private static int rewardPerClick
    {
        get => PlayerPrefs.GetInt("CurrentRewardPerClick", 0);
        set => PlayerPrefs.SetInt("CurrentRewardPerClick", value);
    }

    public void SetValueRewardPerClick(int countPerClick)
    {
        rewardPerClick = countPerClick;
    }
}
