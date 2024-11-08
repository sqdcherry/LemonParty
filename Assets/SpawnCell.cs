using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCell : MonoBehaviour
{
    [SerializeField] private bool isEmpty = true;

    public bool GetIsEmpty()
    {
        if (isEmpty)
            return true;
        else
            return false;
    }

    public void SetIsEmpty(bool value)
    {
        isEmpty = value;
    }
}
