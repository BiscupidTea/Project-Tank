using UnityEngine;

/// <summary>
/// Show Patrol points
/// </summary>
public class PatrolPointsFlag : MonoBehaviour
{
    [SerializeField] private bool hide;
    [SerializeField] private float radius;
    [SerializeField] private Color gizmoColor = Color.blue;
    private void OnDrawGizmos()
    {
        if (!hide)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
