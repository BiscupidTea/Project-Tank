using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _CameraLockPositionTurret;
    [SerializeField] private Transform _CameraLockPositionAim;
    [SerializeField] private Transform turret;
    [SerializeField] private Transform cannon;

    private float actualcannonAngle = 0;
    [SerializeField] float cannonAngleMax = 30;
    [SerializeField] float cannonAngleMin = -10;

    [SerializeField] private Vector2 rotationSpeedCamera;
    [SerializeField] private Vector2 rotationSpeedTurret;

    [SerializeField] private bool LockTurret;
    [SerializeField] private bool isAiming;

    private bool firstTimeLoad = true;
    private int firstTimeLoadCounter = 0;

    private Vector2 scaledDelta;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!LockTurret)
        {
            turret.transform.RotateAround(turret.position, turret.up, scaledDelta.x * rotationSpeedTurret.x);
            cannon.transform.localRotation = Quaternion.Euler(-actualcannonAngle, 0f, 0f);

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
            FreeCameraRotation();
        }

        if (LockTurret && isAiming)
        {
            LockTurret = false;
        }
    }

    private void FreeCameraRotation()
    {
        _camera.transform.RotateAround(turret.position, turret.up, scaledDelta.x);
        _camera.transform.RotateAround(turret.position, turret.right, scaledDelta.y);
        _camera.transform.LookAt(turret);
    }

    public void OnMoveCamera(InputAction.CallbackContext ctx)
    {
        if (!firstTimeLoad)
        {
            var input = ctx.ReadValue<Vector2>();
            scaledDelta = Vector2.Scale(input, rotationSpeedCamera) * Time.deltaTime;
            actualcannonAngle += scaledDelta.y * rotationSpeedTurret.y;
            LimitCannonRotation();
        }
        else
        {
            firstTimeLoadCounter++;
        }

        if (firstTimeLoadCounter == 20)
        {
            firstTimeLoad = false;
        }
    }

    public void FreeCamera(InputAction.CallbackContext input)
    {
        LockTurret = !LockTurret;
    }

    public void Aim(InputAction.CallbackContext input)
    {
        isAiming = !isAiming;
    }

    private void LimitCannonRotation()
    {
        actualcannonAngle = Mathf.Clamp(actualcannonAngle, cannonAngleMin, cannonAngleMax);
    }
}
