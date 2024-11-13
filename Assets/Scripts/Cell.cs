using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private bool _isEmpty = true;

    public bool GetIsEmpty()
    {
        if (_isEmpty)
            return true;
        else
            return false;
    }

    public void SetIsEmpty(bool value)
    {
        _isEmpty = value;
    }
}