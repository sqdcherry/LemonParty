using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private TMP_Text lemonsCountText;

    private int _lemonsCount
    {
        get => PlayerPrefs.GetInt("LemonsCount", 0);
        set => PlayerPrefs.SetInt("LemonsCount", value);
    }

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        lemonsCountText.text = _lemonsCount.ToString();
    }

    public void UpdateLemonsCountText(int lemonsCount)
    {
        _lemonsCount += lemonsCount;
        lemonsCountText.text = _lemonsCount.ToString();
    }
}
