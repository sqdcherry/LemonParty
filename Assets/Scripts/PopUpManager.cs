using TMPro;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager instance;

    [SerializeField] private GameObject PopUp;
    [SerializeField] private TMP_Text PopUpText;

    private Animator anim;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        anim = PopUp.GetComponent<Animator>();
    }

    public void SetActive()
    {
        PopUp.SetActive(false);
    }

    public void StartPopUpAnimation(string text)
    {
        PopUpText.text = text;
        anim.SetTrigger("Pop-up");
    }
}
