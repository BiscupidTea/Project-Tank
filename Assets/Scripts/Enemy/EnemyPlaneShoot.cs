using UnityEngine;
using UnityEngine.Events;

public class EnemyPlaneShoot : MonoBehaviour
{
    [SerializeField] private int viewRange;
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackPlayerDistance;
    [SerializeField] public UnityEvent<GameObject> attackPlayer;
    [SerializeField] private Weapon cannon;
    [SerializeField] private Transform shootPosition;

    private GameObject objective;
    private GameObject player;
    private bool attacking;
    private bool startAttack;
    private bool stopTimer;
    private float timer;

    public int ViewRange { get => viewRange; set => viewRange = value; }
    public float AttackDelay { get => attackDelay; set => attackDelay = value; }
    public float AttackPlayerDistance { get => attackPlayerDistance; set => attackPlayerDistance = value; }
    public Weapon Cannon { get => cannon; set => cannon = value; }
    public Transform ShootPosition { get => shootPosition; set => shootPosition = value; }

    public void StartPlaneBasics(GameObject objectivePlane, GameObject playerPosition)
    {
        objective = objectivePlane;
        player = playerPosition;
        timer = 0;
        attacking = false;
        startAttack = false;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= viewRange)
        {
            if (timer >= AttackDelay)
            {
                startAttack = true;
            }

            if (startAttack)
            {
                AttackPlayer();
                startAttack = false;
                attacking = true;
            }
        }

        if (!attacking && timer < AttackDelay)
        {
            timer += Time.deltaTime;
        }
    }

    public void Shoot(GameObject Objective)
    {
        ShootPosition.LookAt(player.transform);
        Cannon.Shoot();
        attacking = false;
        timer = 0;
    }

    public virtual void AttackPlayer()
    {
        attackPlayer.Invoke(objective);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRange);
    }
}
