using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// player input controller
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private PlayerMovement playerMovemnt;
    [SerializeField] private PauseSystem pauseSystem;
    [SerializeField] private PlayerCamera playerCamera;

    public void ShootInput(InputAction.CallbackContext input)
    {
        if (!pauseSystem.IsPause)
        {
            playerShoot.ShootWeapon();
        }
    }

    public void ChangeWeapon(InputAction.CallbackContext input)
    {
        if (!pauseSystem.IsPause)
        {
            if (input.performed)
            {
                playerShoot.SwitchToNextWeapon();
            }
        }
    }
    /// <summary>
    /// Take input for move player forward and back
    /// </summary>
    /// <param name="input"></param>
    public void OnMoveFB(InputAction.CallbackContext input)
    {
        if (!pauseSystem.IsPause)
        {
            float ActualInput = input.ReadValue<float>();
            playerMovemnt.MovePlayerForwardBack(ActualInput);
        }
    }
    /// <summary>
    /// Take input for move player rotation
    /// </summary>
    /// <param name="input"></param>
    public void OnMoveRo(InputAction.CallbackContext input)
    {
        if (!pauseSystem.IsPause)
        {
            float ActualInput = input.ReadValue<float>();
            playerMovemnt.MovePlayerRightLeft(ActualInput);
        }
    }

    public void ChangePause(InputAction.CallbackContext input)
    {
        pauseSystem.SwitchPause();
    }

    public void OnMoveCamera(InputAction.CallbackContext ctx)
    {
        if (!pauseSystem.IsPause)
        {
            Vector2 input = ctx.ReadValue<Vector2>();
            playerCamera.MoveCamera(input);
        }
    }

    public void FreeCamera(InputAction.CallbackContext input)
    {
        if (!pauseSystem.IsPause)
        {
            playerCamera.ChangeCameraState();
        }
    }

    public void Aim(InputAction.CallbackContext input)
    {
        if (!pauseSystem.IsPause)
        {
            if (input.performed)
            {
                playerCamera.ChangeAimState();
            }
        }
    }
}