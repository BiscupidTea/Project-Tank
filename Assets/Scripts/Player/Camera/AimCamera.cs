using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCamera : CameraBehavior
{
    [SerializeField] private Transform aimPosition;

    public override void RotateCamera(Vector2 input)
    {
        CameraPosition.position = aimPosition.transform.position;
        CameraPosition.rotation = aimPosition.transform.rotation;
    }
}
