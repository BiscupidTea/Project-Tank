using UnityEngine;

public class FreeCamera : CameraBehavior
{
    [SerializeField] private Transform turret;

    public override void RotateCamera(Vector2 input)
    {
        CameraUsed.transform.RotateAround(turret.position, turret.up, ScaledDelta.x);
        CameraUsed.transform.RotateAround(turret.position, turret.right, ScaledDelta.y);
        CameraUsed.transform.LookAt(turret);
    }
}
