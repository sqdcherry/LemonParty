using System.Collections;
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

    void Update()
    {
        StartCoroutine(PasiveCollect());
    }

    private IEnumerator PasiveCollect()
    {
        if (isReadyToCollect)
        {
            isReadyToCollect = false;
            yield return new WaitForSeconds(60f);
            UIManager.instance.UpdateLemonsCountText(pasiveLemons);
            // lemons anim
            isReadyToCollect = true;
        }
    }
}
