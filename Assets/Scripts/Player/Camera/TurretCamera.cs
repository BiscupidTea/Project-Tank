using UnityEngine;

/// <summary>
/// Camera that moves the player turret
/// </summary>
public class TurretCamera : CameraBehavior
{
    [SerializeField] private Transform turret;
    [SerializeField] private Transform cannon;
    [SerializeField] float cannonAngleMax = 30;
    [SerializeField] float cannonAngleMin = -10;

    private float actualcannonAngle = 0;

    public override void RotateCamera(Vector2 input)
    {
        turret.transform.RotateAround(turret.position, turret.up, ScaledDelta.x * RotationSpeedCamera.x);
        cannon.transform.localRotation = Quaternion.Euler(-actualcannonAngle, 0f, 0f);

        CameraUsed.transform.position = CameraPosition.transform.position;
        CameraUsed.transform.rotation = CameraPosition.rotation;

        LimitCannonRotation();
    }
    /// <summary>
    /// Limit the cannon rotation with cannonAngleMin and  cannonAngleMax
    /// </summary>
    private void LimitCannonRotation()
    {
        actualcannonAngle = Mathf.Clamp(actualcannonAngle, cannonAngleMin, cannonAngleMax);
        actualcannonAngle += ScaledDelta.y * RotationSpeedCamera.y;
    }
}
