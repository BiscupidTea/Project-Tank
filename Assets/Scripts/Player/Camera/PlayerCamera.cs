using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Player camera controller that manage 3 types of cameras
/// </summary>
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private CameraBehavior actualCamera;
    [SerializeField] private Slider sensibilitySlider;
    private bool usingController = false;
    public CameraBehavior ActualCamera { get => actualCamera; set => actualCamera = value; }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        var controllers = Input.GetJoystickNames();

        if (!usingController && controllers.Length > 0)
        {
            usingController = true;
            actualCamera.UsingController = usingController;

        }
        else if (usingController && controllers.Length == 0)
        {
            usingController = false;
            actualCamera.UsingController = usingController;
        }

        actualCamera.SensibilitySlider = sensibilitySlider.value;
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
