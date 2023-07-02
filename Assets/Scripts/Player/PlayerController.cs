using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private PlayerMovement playerMovemnt;
    [SerializeField] private PauseSystem pauseSystem;
    [SerializeField] private PlayerCamera playerCamera;

    public void ShootInput(InputAction.CallbackContext input)
    {
        playerShoot.ShootWeapon();
    }

    public void ChangeWeapon(InputAction.CallbackContext input)
    {
        if (input.performed)
        {
            playerShoot.SwitchToNextWeapon();
        }
    }

    public void OnMoveFB(InputAction.CallbackContext input)
    {
        float ActualInput = input.ReadValue<float>();
        playerMovemnt.MovePlayerForwardBack(ActualInput);
    }

    public void OnMoveRo(InputAction.CallbackContext input)
    {
        float ActualInput = input.ReadValue<float>();
        playerMovemnt.MovePlayerRightLeft(ActualInput);
    }

    public void ChangePause(InputAction.CallbackContext input)
    {
        pauseSystem.SwitchPause();
    }

    public void OnMoveCamera(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        playerCamera.MoveCamera(input);
    }

    public void FreeCamera(InputAction.CallbackContext input)
    {
        playerCamera.ChangeCameraState();
    }

    public void Aim(InputAction.CallbackContext input)
    {
        if (input.performed)
        {
            playerCamera.ChangeAimState();
        }
    }
}