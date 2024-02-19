using UnityEngine;
using UnityEngine.Events;

public class EnemyPlaneMovement : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] public UnityEvent<GameObject> finishPlayer;
    [SerializeField] private float minDistancePatrolPoints;
    [SerializeField] private float minDistancePlayer;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    private float actualMinDistance;
    private int targetPoint;
    private bool patrolling;
    private Transform objective;

    public Transform[] PatrolPoints { get => patrolPoints; set => patrolPoints = value; }
    public bool Patrolling { get => patrolling; set => patrolling = value; }
    public Transform Objective { get => objective; set => objective = value; }
    public float MinDistancePatrolPoints { get => minDistancePatrolPoints; set => minDistancePatrolPoints = value; }
    public float MinDistancePlayer { get => minDistancePlayer; set => minDistancePlayer = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }

    public void StartPlaneBasics()
    {
        patrolling = true;
        targetPoint = 0;
        objective = PatrolPoints[targetPoint];
        actualMinDistance = minDistancePatrolPoints;
    }
    private void Update()
    {
        MoveToObjective();

        OrientateToObjective();

        if (Vector3.Distance(transform.position, objective.position) < actualMinDistance)
        {
            if (Patrolling)
            {
                targetPoint++;
                if (targetPoint == PatrolPoints.Length)
                {
                    targetPoint = 0;
                }
            }
            else
            {
                patrolling = true;
                finishPlayer.Invoke(this.gameObject);
            }

            objective = PatrolPoints[targetPoint];

            actualMinDistance = MinDistancePatrolPoints;
        }
    }

    private void MoveToObjective()
    {
        transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime, Space.Self);
    }

    private void OrientateToObjective()
    {
        Vector3 direcion = (objective.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direcion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime);
    }

    public void SetAttackPlayer(GameObject player)
    {
        objective = player.transform;
        actualMinDistance = minDistancePlayer;
        patrolling = false;
    }
}
