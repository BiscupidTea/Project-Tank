using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// player input controller
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("System")]
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private PlayerMovement playerMovemnt;
    [SerializeField] private PauseSystem pauseSystem;
    [SerializeField] private PlayerCamera playerCamera;

    [Header("Cameras")]
    [SerializeField] private CameraBehavior freeCamera;
    [SerializeField] private CameraBehavior turretCamera;
    [SerializeField] private CameraBehavior aimCamera;
    [SerializeField] private CameraBehavior artilleryCamera;

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

    public void ChangeArtilleyCamera(InputAction.CallbackContext input)
    {
        artilleryCamera.GetComponent<ArtilleryCamera>().ChangeStateCamera();
    }

    public void OnMoveCamera(InputAction.CallbackContext ctx)
    {
        if (!pauseSystem.IsPause)
        {
            Vector2 input = ctx.ReadValue<Vector2>();
            playerCamera.MoveCamera(input);
        }
    }
    /// <summary>
    /// Set player camera to free camera or turret camera
    /// </summary>
    /// <param name="input"></param>
    public void FreeCamera(InputAction.CallbackContext input)
    {
        if (!pauseSystem.IsPause)
        {
            if (input.performed)
            {
                if (playerCamera.ActualCamera == freeCamera)
                {
                    playerCamera.ActualCamera = turretCamera;
                }
                else
                {
                    playerCamera.ActualCamera = freeCamera;
                }
                Debug.Log("Change Camera to: " + playerCamera.ActualCamera);
            }
        }
    }
    /// <summary>
    /// Set the player's camera to the aiming camera or the turret camera
    /// </summary>
    /// <param name="input"></param>
    public void Aim(InputAction.CallbackContext input)
    {
        if (!pauseSystem.IsPause)
        {
            if (input.performed)
            {
                if (playerCamera.ActualCamera == aimCamera)
                {
                    playerCamera.ActualCamera = turretCamera;
                    playerCamera.IsAiming = false;
                }
                else
                {
                    playerCamera.ActualCamera = aimCamera;
                    playerCamera.IsAiming = true;
                }
                Debug.Log("Change Camera to: " + playerCamera.ActualCamera);
            }
        }
    }
}