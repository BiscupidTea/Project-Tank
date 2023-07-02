using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : CameraBehavior
{
    [SerializeField] private Transform turret;

    public override void RotateCamera(Vector2 input)
    {
        CameraPosition.RotateAround(turret.position, turret.up, ScaledDelta.x);
        CameraPosition.RotateAround(turret.position, turret.right, ScaledDelta.y);
        CameraPosition.LookAt(turret);
    }
}
