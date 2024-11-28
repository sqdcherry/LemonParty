using System.Collections;
using UnityEngine;

public class Car : MonoBehaviour
{
    private bool isReadyToCollect = true;

    private int pasiveLemons
    {
        get => PlayerPrefs.GetInt("carPasiveLemons", 0);
        set => PlayerPrefs.SetInt("carPasiveLemons", value);
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
            yield return new WaitForSeconds(60);
            UIManager.instance.UpdateLemonsCountText(pasiveLemons);
            // lemons anim
            isReadyToCollect = true;
        }
    }
}
