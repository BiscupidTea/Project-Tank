using UnityEngine;

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
        isAiming = false;
    }

    public void MoveCamera(Vector2 input)
    {
        if (LockTurret)
        {
            if (IsAiming)
            {
                aimCamera.CalculateScaledDelta(input, aimCamera.RotationSpeedCamera);
                aimCamera.RotateCamera(input);
            }
            else
            {
                turretCamera.CalculateScaledDelta(input, turretCamera.RotationSpeedCamera);
                turretCamera.RotateCamera(input);
            }
        }
        else
        {
            freeCamera.CalculateScaledDelta(input, freeCamera.RotationSpeedCamera);
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
