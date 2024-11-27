using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Basket : MonoBehaviour
{
    private PlayerInputs inputs;
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private TMP_Text _lemonsCountText;

    private int collectedLemons;

    private void Awake()
    {
        inputs = new PlayerInputs();
    }

    private void Moving(InputAction.CallbackContext context)
    {
        Vector2 screenCoordinates = inputs.Phone.Moving.ReadValue<Vector2>();
        Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint(screenCoordinates);
        transform.position = new Vector3(worldCoordinates.x, transform.position.y, Camera.main.nearClipPlane);
        worldCoordinates.z = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lemon"))
        {
            collectedLemons += 50;
            _lemonsCountText.text = collectedLemons.ToString();
            AudioManager.instance.PlaySoundEffect(_collectSound);
            Destroy(collision.gameObject);
        }
    }

    private void OnEnable()
    {
        inputs.Enable();

        inputs.Phone.Moving.performed += Moving;
    }

    private void OnDisable()
    {
        inputs.Disable();
        UIManager.instance.UpdateLemonsCountText(collectedLemons);
        inputs.Phone.Moving.performed -= Moving;
    }
}