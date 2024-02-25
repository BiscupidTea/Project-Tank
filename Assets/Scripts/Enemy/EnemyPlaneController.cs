using UnityEngine;
using UnityEngine.Events;

public class EnemyPlaneController : MonoBehaviour, IHealthComponent
{
    [SerializeField] public UnityEvent<GameObject> onDeath;
    [SerializeField] private EnemyPlaneShoot flyEnemyAttack;
    [SerializeField] private EnemyPlaneMovement flyEnemyMovement;
    private string id;
    private float currentHealth;
    protected bool isAlive;

    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public EnemyPlaneShoot FlyEnemyAttack { get => flyEnemyAttack; set => flyEnemyAttack = value; }
    public EnemyPlaneMovement FlyEnemyMovement { get => flyEnemyMovement; set => flyEnemyMovement = value; }
    public string Id { get => id; set => id = value; }

    public void AddListenersToController()
    {
        FlyEnemyAttack.attackPlayer.AddListener(StartAttackPLayer);
        FlyEnemyMovement.finishPlayer.AddListener(shootPlayer);
    }

    private void OnDisable()
    {
        FlyEnemyAttack.attackPlayer.RemoveAllListeners();
        FlyEnemyMovement.finishPlayer.RemoveAllListeners();
    }

    private void StartAttackPLayer(GameObject player)
    {
        FlyEnemyMovement.SetAttackPlayer(player);
    }

    private void shootPlayer(GameObject player)
    {
        FlyEnemyAttack.Shoot(player);
    }

    public virtual void Death()
    {
        onDeath.Invoke(this.gameObject);
    }

    [ContextMenu("Kill Enemy")]
    public void KillEnemy()
    {
        currentHealth = 0;
        isAlive = false;
        Death();
    }

    /// <summary>
    /// Decreases current health taking damage
    /// </summary>
    /// <param name="damage"></param>
    public void ReceiveDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            isAlive = false;
            Death();
        }
    }

    public virtual bool IsAlive()
    {
        return isAlive;
    }

    public void SetIsAlive(bool status)
    {
        isAlive = status;
    }
}
