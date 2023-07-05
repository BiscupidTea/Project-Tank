using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Camera input for controller
/// </summary>
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
        playerCamera.MoveCamera(currentDelta * Time.deltaTime);
    }
}
