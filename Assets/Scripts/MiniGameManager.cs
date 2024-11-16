using System;
using System.Collections;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private GameObject _lemonPrefab;
    [SerializeField] private Transform _lemonSpawnPos;

    private bool canSpawn = true;

    private void Update()
    {
        StartCoroutine(SpawnLemons());
    }

    private IEnumerator SpawnLemons()
    {
        if (canSpawn)
        {
            canSpawn = false;
            Vector2 spawnPos = new Vector2(UnityEngine.Random.Range(_lemonSpawnPos.position.x - 2f, _lemonSpawnPos.position.x + 2f),
                _lemonSpawnPos.transform.position.y);
            Quaternion spawnQuternion = Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(0, 360)));

            GameObject currentLemon = Instantiate<GameObject>(_lemonPrefab, spawnPos, spawnQuternion, _lemonSpawnPos); 
            yield return new WaitForSeconds(1f);
            canSpawn = true;
        }
    }
}