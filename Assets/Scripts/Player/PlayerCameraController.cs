using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _CameraLockPositionTurret;
    [SerializeField] private Transform _CameraLockPositionAim;
    [SerializeField] private Transform turret;
    [SerializeField] private Transform cannon;

    [SerializeField] private Vector2 rotationSpeedCamera;
    [SerializeField] private Vector2 rotationSpeedTurret;

    [SerializeField] private bool LockTurret;
    [SerializeField] private bool isAiming;

    private Vector2 scaledDelta;

    private void Update()
    {
        if (!LockTurret)
        {
            turret.transform.RotateAround(turret.position, turret.up, scaledDelta.x);
            cannon.transform.Rotate(scaledDelta.y, 0, 0);

            if (isAiming)
            {
                _camera.transform.position = _CameraLockPositionAim.transform.position;
                _camera.transform.rotation = _CameraLockPositionAim.transform.rotation;
            }
            else
            {
                _camera.transform.position = _CameraLockPositionTurret.transform.position;
                _camera.transform.rotation = _CameraLockPositionTurret.transform.rotation;
            }
        }
        else
        {
            _camera.transform.RotateAround(turret.position, turret.up, scaledDelta.x);
            _camera.transform.RotateAround(turret.position, turret.up, scaledDelta.y);
        }
    }
    public void OnMoveCamera(InputAction.CallbackContext ctx)
    {
        var input = ctx.ReadValue<Vector2>();
        scaledDelta = Vector2.Scale(input, rotationSpeedCamera) * Time.deltaTime;
    }

    public void FreeCamera(InputAction.CallbackContext input)
    {
        LockTurret = !LockTurret;
    }

    public void Aim(InputAction.CallbackContext input)
    {
        isAiming = !isAiming;
    }
}
