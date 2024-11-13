using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject lemonPerent;
    [SerializeField] private List<Cell> spawnPosList;
    [SerializeField] private List<GameObject> lemonsList;
    [SerializeField] private List<GameObject> boughtLemonsList;

    private bool isReadyToSpawn = true;

    private int spawnDelay
    {
        get => PlayerPrefs.GetInt("SpawnRate", 0);
        set => PlayerPrefs.SetInt("SpawnRate", value);
    }

    private static int rewardPerClick
    {
        get => PlayerPrefs.GetInt("CurrentRewardPerClick", 0);
        set => PlayerPrefs.SetInt("CurrentRewardPerClick", value);
    }

    public void SetValueRewardPerClick(int countPerClick)
    {
        rewardPerClick = countPerClick;
    }

    private void Start()
    {
        for (int i = 0; i < lemonsList.Count; i++)
        {
            if (lemonsList[i].GetComponent<LemonTree>().GetCollectableStage())
                SetBoughtLemonsList(lemonsList[i]);
        }
    }

    public void SetBoughtLemonsList(GameObject boughtLemonTree)
    {
        boughtLemonsList.Add(boughtLemonTree);
    }

    public void Collect()
    {
        int currentColectItem = Random.Range(0, boughtLemonsList.Count);
        boughtLemonsList[currentColectItem].GetComponent<LemonTree>().Collected();    
    }

    public void BuyPlot(LemonTree currentLimonTree)
    {
        currentLimonTree.BuyPlot();
        boughtLemonsList.Add(currentLimonTree.gameObject);
    }
}