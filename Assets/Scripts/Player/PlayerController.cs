using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

/// <summary>
/// player input controller
/// </summary>
public class PlayerController : MonoBehaviour, IHealthComponent
{
    public UnityEvent<GameObject> onDeath;
    public UnityEvent<GameObject> onAim;
    public UnityEvent<float> onTakeDamage;
    public UnityEvent<GameObject> onSwitchWeapon;
    public UnityEvent<GameObject> activateCheats;
    public UnityEvent<GameObject> continueTutorial;
    public UnityEvent<string> sendCheatCode;
    [SerializeField] private float currentHealth;
    protected bool isAlive;
    private bool immortal;
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool Immortal { get => immortal; set => immortal = value; }

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

    private void Start()
    {
        isAlive = true;
    }

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
                if (playerCamera.ActualCamera == artilleryCamera)
                {
                    playerCamera.ActualCamera = turretCamera;
                }

                playerShoot.SwitchToNextWeapon();
            }

            onSwitchWeapon.Invoke(this.gameObject);
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

    public void ChangeArtilleryCamera(InputAction.CallbackContext input)
    {
        if (!pauseSystem.IsPause)
        {
            if (input.performed)
            {
                if (playerCamera.ActualCamera == artilleryCamera)
                {
                    playerCamera.ActualCamera = turretCamera;
                }
                else
                {
                    playerCamera.ActualCamera = artilleryCamera;
                    playerCamera.ActualCamera.SetCameraValues();
                }
                onAim.Invoke(this.gameObject);
                Debug.Log("Change Camera to: " + playerCamera.ActualCamera);
            }

        }
    }

    public void ChangeArtilleryWeapon(InputAction.CallbackContext input)
    {
        if (!pauseSystem.IsPause)
        {
            playerShoot.SwitchToArtillery();
            onSwitchWeapon.Invoke(this.gameObject);
        }
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
                    playerCamera.ActualCamera.SetCameraValues();
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
                if (playerShoot.WeaponInUse.id != "Artillery")
                {

                    if (playerCamera.ActualCamera == aimCamera)
                    {
                        playerCamera.ActualCamera = turretCamera;
                    }
                    else
                    {
                        playerCamera.ActualCamera = aimCamera;
                        playerCamera.ActualCamera.SetCameraValues();
                    }
                    Debug.Log("Change Camera to: " + playerCamera.ActualCamera);
                    onAim.Invoke(this.gameObject);
                }
            }
        }
    }

    public void ActiveCheats(InputAction.CallbackContext input)
    {
        if (input.performed)
        {
            activateCheats.Invoke(this.gameObject);
        }
    }

    public void NextLevelCheat(InputAction.CallbackContext input)
    {
        if (input.performed)
        {
            sendCheatCode.Invoke("NextLevel");
        }
    }

    public void GodModeCheat(InputAction.CallbackContext input)
    {
        if (input.performed)
        {
            sendCheatCode.Invoke("GodMode");
        }
    }

    public void FlashCheat(InputAction.CallbackContext input)
    {
        if (input.performed)
        {
            sendCheatCode.Invoke("Flash");
        }
    }

    public void NukeCheat(InputAction.CallbackContext input)
    {
        if (input.performed)
        {
            sendCheatCode.Invoke("Nuke");
        }
    }

    public void ContinueTutorial(InputAction.CallbackContext input)
    {
        if (input.performed)
        {
            continueTutorial.Invoke(this.gameObject);
        }
    }

    [ContextMenu("Kill Player")]
    private void KillPlayer()
    {
        currentHealth = 0;
        isAlive = false;
        Death();
    }

    public virtual void Death()
    {
        onDeath.Invoke(this.gameObject);
    }

    /// <summary>
    /// Decreases current health taking damage
    /// </summary>
    /// <param name="damage"></param>
    public void ReceiveDamage(float damage)
    {
        if (!Immortal)
        {
            CurrentHealth -= damage;
            onTakeDamage.Invoke(currentHealth);

            if (CurrentHealth <= 0)
            {
                isAlive = false;
                Death();
            }
        }
    }

    public virtual bool IsAlive()
    {
        return isAlive;
    }
}