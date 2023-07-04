using UnityEngine;

/// <summary>
/// Base camera
/// </summary>
public abstract class CameraBehavior : MonoBehaviour
{
    [SerializeField] private Camera cameraUsed;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private Vector2 rotationSpeedCamera;

    private Vector2 scaledDelta;

    public Camera CameraUsed { get => cameraUsed; set => cameraUsed = value; }
    public Transform CameraPosition { get => cameraPosition; set => cameraPosition = value; }
    public Vector2 ScaledDelta { get => scaledDelta; set => scaledDelta = value; }
    public Vector2 RotationSpeedCamera { get => rotationSpeedCamera; set => rotationSpeedCamera = value; }

    public abstract void RotateCamera(Vector2 input);

    public void CalculateScaledDelta(Vector2 input, Vector2 rotationSpeedCamera)
    {
        scaledDelta = Vector2.Scale(input, rotationSpeedCamera) * Time.deltaTime;
    }
}
