using System;
using UnityEngine;

public class ArtilleryCamera : CameraBehavior
{
    [SerializeField] private float maxVerticalAngle;
    [SerializeField] private float minVerticalAngle;

    [SerializeField] private float mouseSensitivity;

    public override void SetCameraValues()
    {
        CameraUsed.transform.position = CameraPosition.transform.position;
        CameraUsed.transform.rotation = CameraPosition.transform.rotation;
    }

    public override void RotateCamera(Vector2 input)
    {
        CameraUsed.transform.Rotate(CameraPosition.up, ScaledDelta.x);

        float clampAngles = CameraUsed.transform.localEulerAngles.x - ScaledDelta.y;
        clampAngles = Mathf.Clamp(clampAngles, minVerticalAngle, maxVerticalAngle);

        CameraUsed.transform.localEulerAngles = Vector3.right * clampAngles;
    }
}
