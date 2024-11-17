using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField] private BuyTreePanel panel;
    [SerializeField] private List<GameObject> lemonsList;
    [SerializeField] private List<GameObject> boughtLemonsList;

    private bool isReadyToSpawn = true;
    private LemonTree _currentLimonTree;
    private int _currentPrice;

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
            if (lemonsList[i].GetComponent<LemonTree>().GetCurrentStage() == 3)
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

    public void BuyPlotPanel(LemonTree currentLimonTree)
    {
        int stage = currentLimonTree.GetCurrentStage();
        _currentLimonTree = currentLimonTree;

        if (stage == 0)
        {
            panel.gameObject.SetActive(true);
            panel.headText.text = "BUY A PLOT";
            panel.priceText.text = 100.ToString();
            _currentPrice = 1;
        }
        else if (stage == 1)
        {
            panel.gameObject.SetActive(true);
            panel.headText.text = "DIG UP A PLOT";
            panel.priceText.text = 300.ToString();
            _currentPrice = 3;
        }
        else if (stage == 2)
        {
            panel.gameObject.SetActive(true);
            panel.headText.text = "SOW A PLOT";
            panel.priceText.text = 300.ToString();
            _currentPrice = 5;
        }
    }

    public void BuyPlot()
    {
        if (UIManager.instance.GetLemonsCount() - _currentPrice >= 0)
        {
            _currentLimonTree.BuyPlot();
            UIManager.instance.UpdateLemonsCountText(-_currentPrice);
            panel.gameObject.SetActive(false);

            int currentStage = _currentLimonTree.GetCurrentStage();
            if (currentStage == 3)
                boughtLemonsList.Add(_currentLimonTree.gameObject);
        }
    }
}