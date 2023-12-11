using UnityEngine;

public class ArtilleryCamera : CameraBehavior
{
    [SerializeField] private Transform StartPosition;
    [SerializeField] private Transform EndPosition;

    private bool cameraChangingPosition;
    private bool changeNewPosition;

    public bool CameraInPosition { get => cameraChangingPosition; set => cameraChangingPosition = value; }

    private float timer;

    private void Start()
    {
        CameraInPosition = false;
    }

    public void ChangeStateCamera()
    {
        changeNewPosition = true;
    }

    private void Update()
    {

    }

    public override void RotateCamera(Vector2 input)
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(StartPosition.position, EndPosition.position);
    }
}
