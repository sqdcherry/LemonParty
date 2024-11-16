using UnityEngine;
using UnityEngine.InputSystem;

public class Basket : MonoBehaviour
{
    private PlayerInputs inputs;

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

    private void OnEnable()
    {
        inputs.Enable();

        inputs.Phone.Moving.performed += Moving;
    }

    private void OnDisable()
    {
        inputs.Disable();

        inputs.Phone.Moving.performed -= Moving;
    }
}