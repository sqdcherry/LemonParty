using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    private bool isReadyToCollect = true;

    private int pasiveLemons
    {
        get => PlayerPrefs.GetInt("workerPasiveLemons", 0);
        set => PlayerPrefs.SetInt("workerPasiveLemons", value);
    }

    public void SetPasiveLemons(int value)
    {
        pasiveLemons += value;
    }

    private void Awake()
    {
        //spasiveLemons = 0;
    }

    void Update()
    {
        StartCoroutine(PasiveCollect());
    }

    private IEnumerator PasiveCollect()
    {
        if (isReadyToCollect)
        {
            isReadyToCollect = false;
            UIManager.instance.UpdateLemonsCountText(pasiveLemons);
            // lemons anim
            yield return new WaitForSeconds(1f);
            isReadyToCollect = true;
        }
    }
}
