using UnityEngine;

/// <summary>
/// Player camera controller that manage 3 types of cameras
/// </summary>
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private CameraBehavior actualCamera;
    [SerializeField] private bool isAiming;

    public bool IsAiming { get => isAiming; set => isAiming = value; }
    public CameraBehavior ActualCamera { get => actualCamera; set => actualCamera = value; }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isAiming = false;
    }
    /// <summary>
    /// Camera manager for freeCamera, turretCamera and aimCamera
    /// </summary>
    /// <param name="input"></param>
    public void MoveCamera(Vector2 input)
    {
        actualCamera.CalculateScaledDelta(input);
        actualCamera.RotateCamera(input);
    }
}
