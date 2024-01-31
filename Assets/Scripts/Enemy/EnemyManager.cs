using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private TankSpawn[] tankSpawnList;
    [SerializeField] Transform enemyParent;
    [SerializeField] GameObject BaseEnemy;
    [SerializeField] private List<GameObject> enemySpawned;

    private Dictionary<string, ObjectPool<GameObject>> EnemyPoolById = new();
    private EnemyFactory enemyFactory = new EnemyFactory();

    public List<GameObject> EnemySpawned { get => enemySpawned; set => enemySpawned = value; }

    private void Start()
    {
        foreach (var e in tankSpawnList)
        {
            if (EnemyPoolById.TryAdd(e.EnemyTank.name, new ObjectPool<GameObject>(() => Instantiate(BaseEnemy, enemyParent),
            enemy => { enemy.gameObject.SetActive(true); }, enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); }, false, tankSpawnList.Length, 100)))
            {
                Debug.Log("New Pool: " + e.EnemyTank.name);
            }
            else
            {
                Debug.Log(e.EnemyTank.name + ": pool Already Exist");
            }
        }

        for (int i = 0; i < tankSpawnList.Length; i++)
        {
            addNewEnemy(tankSpawnList[i]);
        }
    }

    private void addNewEnemy(TankSpawn tankSpawn)
    {
        var pool = EnemyPoolById[tankSpawn.EnemyTank.name];
        if (pool == null)
        {
            Debug.LogError("Enemy Pool not found");
            return;
        }

        GameObject newEnemy = null;
        pool.Get(out newEnemy);
        newEnemy.GetComponent<EnemyController>().onDeath.AddListener(OnKillEnemy);

        NavMeshHit Hit;
        if (NavMesh.SamplePosition(tankSpawn.SpawnPoint.position, out Hit, 2f, 1))
        {
            newEnemy.GetComponent<EnemyMove>().Enemy.Warp(Hit.position);
            newEnemy.GetComponent<EnemyMove>().Enemy.enabled = true;
        }
        else
        {
            Debug.LogError("Unable to place NavMeshAgent on NavMesh");
        }

        if (tankSpawn.PatrolPoints.Length < 1)
        {
            enemyFactory.NewEnemyConfigure(ref newEnemy, tankSpawn.EnemySo, tankSpawn.SpawnPoint);

        }
        else
        {
           enemyFactory.NewEnemyConfigure(ref newEnemy, tankSpawn.EnemySo, tankSpawn.SpawnPoint, tankSpawn.PatrolPoints);
        }

        EnemySpawned.Add(newEnemy);
        Debug.Log("Enemy Created! total Enemies = " + enemySpawned.Count);
    }

    private void OnKillEnemy(GameObject enemy)
    {
        EnemyController baseEnemy = enemy.GetComponent<EnemyController>();
        baseEnemy.onDeath.RemoveListener(OnKillEnemy);
        ObjectPool<GameObject> pool = EnemyPoolById[baseEnemy.Id];
        pool.Release(enemy);
        EnemySpawned.Remove(enemy);
    }
}

[System.Serializable]
public class TankSpawn
{
    public GameObject EnemyTank;
    public EnemySO EnemySo;
    public Transform SpawnPoint;
    public Transform[] PatrolPoints;
}