using UnityEngine;

public class AimCamera : CameraBehavior
{
    [SerializeField] private Transform aimPosition;
    [SerializeField] private Transform turret;
    [SerializeField] private Transform cannon;
    [SerializeField] float cannonAngleMax = 30;
    [SerializeField] float cannonAngleMin = -10;

    private float actualcannonAngle = 0;

    public override void RotateCamera(Vector2 input)
    {
        turret.transform.RotateAround(turret.position, turret.up, ScaledDelta.x * RotationSpeedCamera.x);
        cannon.transform.localRotation = Quaternion.Euler(-actualcannonAngle, 0f, 0f);

        CameraUsed.transform.position = aimPosition.transform.position;
        CameraUsed.transform.rotation = aimPosition.transform.rotation;

        LimitCannonRotation();
    }
    private void LimitCannonRotation()
    {
        actualcannonAngle = Mathf.Clamp(actualcannonAngle, cannonAngleMin, cannonAngleMax);
        actualcannonAngle += ScaledDelta.y * RotationSpeedCamera.y;
    }
}
