using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private FreeCamera freeCamera;
    [SerializeField] private TurretCamera turretCamera;
    [SerializeField] private AimCamera aimCamera;
    [SerializeField] private bool LockTurret;
    [SerializeField] private bool isAiming;

    public bool IsAiming { get => isAiming; set => isAiming = value; }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        LockTurret = true;
    }

    public void MoveCamera(Vector2 input)
    {
        if (LockTurret) 
        { 
            if (IsAiming) 
            { 
                aimCamera.RotateCamera(input);
            }
            else
            {
                turretCamera.RotateCamera(input);
            }
        }
        else 
        { 
            freeCamera.RotateCamera(input);
        }
    }

    public void ChangeCameraState()
    {
        LockTurret = !LockTurret;
    }

    public void ChangeAimState()
    {
        IsAiming = !IsAiming;
    }
}
