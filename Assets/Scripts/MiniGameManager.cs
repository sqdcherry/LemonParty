using System;
using System.Collections;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private GameObject _lemonPrefab;
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject _basket;
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private Transform _lemonSpawnPos;

    private bool isReadyToSpawn = true;
    private bool canSpawn;

    private void Update()
    {
        StartCoroutine(SpawnLemons());
    }

    private IEnumerator SpawnLemons()
    {
        if (isReadyToSpawn && canSpawn)
        {
            isReadyToSpawn = false;
            Vector2 spawnPos = new Vector2(UnityEngine.Random.Range(_lemonSpawnPos.position.x - 2f, _lemonSpawnPos.position.x + 2f),
                _lemonSpawnPos.transform.position.y);
            Quaternion spawnQuternion = Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(0, 360)));

            GameObject currentLemon = Instantiate<GameObject>(_lemonPrefab, spawnPos, spawnQuternion, _lemonSpawnPos); 
            yield return new WaitForSeconds(1.1f);
            isReadyToSpawn = true;
        }
    }

    public void SetActive()
    {
        _gameScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _basket.SetActive(true);
        canSpawn = true;
    }

    private void OnDisable()
    {
        _basket.SetActive(false);
        _button.SetActive(false);
        canSpawn = false;
    }
}