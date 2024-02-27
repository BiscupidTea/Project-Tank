using UnityEngine;

/// <summary>
/// Base camera
/// </summary>
public abstract class CameraBehavior : MonoBehaviour
{
    [SerializeField] private Camera cameraUsed;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private Vector2 rotationControllerSpeedCamera;
    [SerializeField] private Vector2 rotationMouseSpeedCamera;
    private bool usingController;
    private float sensibilitySlider;

    private Vector2 scaledDelta;

    public Camera CameraUsed { get => cameraUsed; set => cameraUsed = value; }
    public Transform CameraPosition { get => cameraPosition; set => cameraPosition = value; }
    public Vector2 ScaledDelta { get => scaledDelta; set => scaledDelta = value; }
    public bool UsingController { get => usingController; set => usingController = value; }
    public Vector2 RotationControllerSpeedCamera { get => rotationControllerSpeedCamera; set => rotationControllerSpeedCamera = value; }
    public Vector2 RotationMouseSpeedCamera { get => rotationMouseSpeedCamera; set => rotationMouseSpeedCamera = value; }
    public float SensibilitySlider { get => sensibilitySlider; set => sensibilitySlider = value; }

    public abstract void SetCameraValues();
    public abstract void RotateCamera(Vector2 input);

    public void CalculateScaledDelta(Vector2 input)
    {
        if (usingController)
        {
            scaledDelta = Vector2.Scale(input, RotationControllerSpeedCamera * SensibilitySlider) * Time.deltaTime;
        }
        else
        {
            scaledDelta = Vector2.Scale(input, RotationMouseSpeedCamera * SensibilitySlider) * Time.deltaTime;
        }
    }
}
