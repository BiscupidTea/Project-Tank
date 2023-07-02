using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCamera : CameraBehavior
{
    [SerializeField] private Transform turret;
    [SerializeField] private Transform cannon;
    [SerializeField] private Vector2 rotationSpeedTurret;
    [SerializeField] float cannonAngleMax = 30;
    [SerializeField] float cannonAngleMin = -10;

    private float actualcannonAngle = 0;
    public override void RotateCamera(Vector2 input)
    {
        turret.transform.RotateAround(turret.position, turret.up, ScaledDelta.x * rotationSpeedTurret.x);
        cannon.transform.localRotation = Quaternion.Euler(-actualcannonAngle, 0f, 0f);

        CameraPosition = CameraBasePosition;
        CameraPosition.rotation = CameraBasePosition.rotation;


        LimitCannonRotation();
    }

    private void LimitCannonRotation()
    {
        actualcannonAngle = Mathf.Clamp(actualcannonAngle, cannonAngleMin, cannonAngleMax);
        actualcannonAngle += ScaledDelta.y * rotationSpeedTurret.y;
    }
}
