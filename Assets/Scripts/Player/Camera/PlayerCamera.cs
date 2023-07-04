using UnityEngine;

/// <summary>
/// Player camera controller that manage 3 types of cameras
/// </summary>
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
    /// <summary>
    /// Camera manager for freeCamera, turretCamera and aimCamera
    /// </summary>
    /// <param name="input"></param>
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
    /// <summary>
    /// Change value of lockTurret
    /// </summary>
    public void ChangeCameraState()
    {
        LockTurret = !LockTurret;
    }
    /// <summary>
    /// Change value of isAiming
    /// </summary>
    public void ChangeAimState()
    {
        IsAiming = !IsAiming;
    }
}
