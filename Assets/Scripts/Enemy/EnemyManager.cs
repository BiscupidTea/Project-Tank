using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] public UnityEvent<GameObject> winCondition;
    [SerializeField] private EnemyTankSpawn[] EnemyTankList;
    [SerializeField] private EnemyPlaneSpawn[] EnemyPlaneList;
    [SerializeField] Transform enemyParent;
    [SerializeField] GameObject BaseTankEnemy;
    [SerializeField] GameObject BaseAirEnemy;
    [SerializeField] private List<GameObject> enemySpawned;

    private Dictionary<string, ObjectPool<GameObject>> EnemyPoolById = new();
    private EnemyFactory enemyFactory = new EnemyFactory();

    public List<GameObject> EnemySpawned { get => enemySpawned; set => enemySpawned = value; }

    private void Start()
    {
        foreach (var e in EnemyTankList)
        {
            if (EnemyPoolById.TryAdd(e.EnemySo.Asset.name, new ObjectPool<GameObject>(() => Instantiate(BaseTankEnemy, enemyParent),
            enemy => { enemy.gameObject.SetActive(true); }, enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); }, false, EnemyTankList.Length, 100)))
            {
                Debug.Log("New Pool: " + e.EnemySo.Asset.name);
            }
            else
            {
                Debug.Log(e.EnemySo.Asset.name + ": pool Already Exist");
            }
        }

        for (int i = 0; i < EnemyTankList.Length; i++)
        {
            addNewEnemyTank(EnemyTankList[i]);
        }

        foreach (var e in EnemyPlaneList)
        {
            if (EnemyPoolById.TryAdd(e.EnemySo.Asset.name, new ObjectPool<GameObject>(() => Instantiate(BaseAirEnemy, enemyParent),
            enemy => { enemy.gameObject.SetActive(true); }, enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); }, false, EnemyPlaneList.Length, 100)))
            {
                Debug.Log("New Pool: " + e.EnemySo.Asset.name);
            }
            else
            {
                Debug.Log(e.EnemySo.Asset.name + ": pool Already Exist");
            }
        }

        for (int i = 0; i < EnemyPlaneList.Length; i++)
        {
            addNewEnemyPlane(EnemyPlaneList[i]);
        }
    }

    private void addNewEnemyTank(EnemyTankSpawn NewEnemyTank)
    {
        var pool = EnemyPoolById[NewEnemyTank.EnemySo.Asset.name];
        if (pool == null)
        {
            Debug.LogError("Enemy Pool not found");
            return;
        }

        GameObject newEnemy = null;
        pool.Get(out newEnemy);
        newEnemy.GetComponent<EnemyTankController>().onDeath.AddListener(OnKillEnemyTank);

        NavMeshHit Hit;
        if (NavMesh.SamplePosition(NewEnemyTank.SpawnPoint.position, out Hit, 2f, 1))
        {
            newEnemy.GetComponent<EnemyTankMovement>().Enemy.Warp(Hit.position);
            newEnemy.GetComponent<EnemyTankMovement>().Enemy.enabled = true;
        }
        else
        {
            Debug.LogError("Unable to place NavMeshAgent on NavMesh");
        }

        if (NewEnemyTank.PatrolPoints.Length < 1)
        {
            enemyFactory.NewEnemyTankConfigure(ref newEnemy, NewEnemyTank.EnemySo, NewEnemyTank.SpawnPoint);

        }
        else
        {
            enemyFactory.NewEnemyTankConfigure(ref newEnemy, NewEnemyTank.EnemySo, NewEnemyTank.SpawnPoint, NewEnemyTank.PatrolPoints);
        }

        EnemySpawned.Add(newEnemy);
        Debug.Log("Enemy Created! total Enemies = " + enemySpawned.Count);
    }

    private void addNewEnemyPlane(EnemyPlaneSpawn NewEnemyPlane)
    {
        var pool = EnemyPoolById[NewEnemyPlane.EnemySo.Asset.name];
        if (pool == null)
        {
            Debug.LogError("Enemy Pool not found");
            return;
        }

        GameObject newEnemy = null;
        pool.Get(out newEnemy);
        newEnemy.GetComponent<EnemyPlaneController>().onDeath.AddListener(OnKillEnemyPlane);

        enemyFactory.NewEnemyPlaneConfigure(ref newEnemy, NewEnemyPlane.EnemySo, NewEnemyPlane.SpawnPoint, NewEnemyPlane.PatrolPoints);

        EnemySpawned.Add(newEnemy);
        Debug.Log("Enemy Created! total Enemies = " + enemySpawned.Count);
    }

    private void OnKillEnemyTank(GameObject enemy)
    {
        EnemyTankController baseEnemy = enemy.GetComponent<EnemyTankController>();
        baseEnemy.onDeath.RemoveListener(OnKillEnemyTank);
        ObjectPool<GameObject> pool = EnemyPoolById[baseEnemy.Id];
        pool.Release(enemy);
        EnemySpawned.Remove(enemy);
        CheckWinCondition();
    }

    private void OnKillEnemyPlane(GameObject enemy)
    {
        EnemyPlaneController baseEnemy = enemy.GetComponent<EnemyPlaneController>();
        baseEnemy.onDeath.RemoveListener(OnKillEnemyPlane);
        ObjectPool<GameObject> pool = EnemyPoolById[baseEnemy.Id];
        pool.Release(enemy);
        EnemySpawned.Remove(enemy);
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (enemySpawned.Count <= 0)
        {
            winCondition.Invoke(this.gameObject);
        }
    }

    public void ClearAllEnemies()
    {
        for (int i = enemySpawned.Count - 1; i >= 0; i--)
        {
            GameObject enemyObject = enemySpawned[i];
            EnemyTankController tankController = enemyObject.GetComponent<EnemyTankController>();
            EnemyPlaneController planeController = enemyObject.GetComponent<EnemyPlaneController>();

            if (tankController != null)
            {
                tankController.KillEnemy();
            }
            else if (planeController != null)
            {
                planeController.KillEnemy();
            }
            else
            {
                Debug.Log("error enemy dont have kill method");
            }
        }
    }
}

[System.Serializable]
public class EnemyTankSpawn
{
    public EnemyTankSO EnemySo;
    public Transform SpawnPoint;
    public Transform[] PatrolPoints;
}

[System.Serializable]
public class EnemyPlaneSpawn
{
    public EnemyPlaneSO EnemySo;
    public Transform SpawnPoint;
    public Transform[] PatrolPoints;
}