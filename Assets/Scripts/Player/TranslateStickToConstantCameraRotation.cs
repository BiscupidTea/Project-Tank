using UnityEngine;
using UnityEngine.InputSystem;

public class TranslateStickToConstantCameraRotation : MonoBehaviour
{
    [SerializeField] private PlayerCamera playerCamera;
    private Vector2 currentDelta;
    public void OnSetCameraConstantRotation(InputAction.CallbackContext input)
    {
        currentDelta = input.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        playerCamera.MoveCamera(currentDelta);
    }
}
