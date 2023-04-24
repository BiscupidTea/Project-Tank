using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 rotationSpeed;
    public void OnMoveCamera(InputAction.CallbackContext input)
    {
      //  var scaledDelta = Vector2.Scale(input, rotationSpeed) * Time.deltaTime;

        //_camera.transform.RotateAround(target.position, target.up, scaledDelta.x);
       // _camera.transform.RotateAround(target.position, target.right, scaledDelta.y);

        Debug.Log("enter");
    }
}
