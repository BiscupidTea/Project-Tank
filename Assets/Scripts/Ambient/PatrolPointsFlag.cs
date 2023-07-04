using UnityEngine;

/// <summary>
/// Show Patrol points
/// </summary>
public class PatrolPointsFlag : MonoBehaviour
{
    [SerializeField] private bool showPosition;
    [SerializeField] private float radius;
    private void OnDrawGizmos()
    {
        if (showPosition)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
