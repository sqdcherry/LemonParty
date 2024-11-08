using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject limonPrefab;
    [SerializeField] private GameObject limonPerent;
    [SerializeField] private List<SpawnCell> spawnPosList;
    [SerializeField] private List<GameObject> limonList;

    private bool isReadyToSpawn = true;

    private int spawnDelay
    {
        get => PlayerPrefs.GetInt("SpawnRate", 0);
        set => PlayerPrefs.SetInt("SpawnRate", value);
    }


    void Update()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        int currentPosIndex = Random.Range(0, spawnPosList.Count);
        bool isCanSpawn = false;

        if (spawnPosList[currentPosIndex].GetIsEmpty())
        {
            do
            {
                currentPosIndex = Random.Range(0, spawnPosList.Count);
                if (spawnPosList[currentPosIndex].GetIsEmpty())
                    isCanSpawn = true;
            }
            while (!isCanSpawn);
        }

        if (isReadyToSpawn && isCanSpawn)
        { 
            var currentItem = Instantiate<GameObject>(limonPrefab, spawnPosList[currentPosIndex].transform.position, Quaternion.identity, spawnPosList[currentPosIndex].transform);
            spawnPosList[currentPosIndex].SetIsEmpty(false);
            limonList.Add(currentItem);
            isReadyToSpawn = false;
            yield return new WaitForSeconds((float)spawnDelay);
            isReadyToSpawn = true;
        }
    }

    public void Collect()
    {
        if (limonList.Count != 0)
        {
            int currentColectItem = Random.Range(0, limonList.Count);
            limonList[currentColectItem].transform.parent.GetComponent<SpawnCell>().SetIsEmpty(true);
            Destroy(limonList[currentColectItem].gameObject);
            limonList.Remove(limonList[currentColectItem]);

            UIManager.instance.UpdateLemonsCountText(1);
        }
    }
}
