using UnityEngine;

public abstract class CameraBehavior : MonoBehaviour
{
    [SerializeField] private Camera cameraUsed;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private Transform cameraBasePosition;
    [SerializeField] private Vector2 rotationSpeedCamera;

    private Vector2 scaledDelta;

    public Transform CameraPosition { get => cameraPosition; set => cameraPosition = value; }
    public Transform CameraBasePosition { get => cameraBasePosition; set => cameraBasePosition = value; }
    public Vector2 ScaledDelta { get => scaledDelta; set => scaledDelta = value; }
    public Vector2 RotationSpeedCamera { get => rotationSpeedCamera; set => rotationSpeedCamera = value; }

    public abstract void RotateCamera(Vector2 input);

    public void CalculateScaledDelta(Vector2 input, Vector2 rotationSpeedCamera)
    {
        scaledDelta = Vector2.Scale(input, rotationSpeedCamera) * Time.deltaTime;
    }
}
