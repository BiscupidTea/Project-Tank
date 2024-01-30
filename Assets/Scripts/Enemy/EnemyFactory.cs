using UnityEngine;

public class EnemyFactory
{

    public void NewEnemyConfigure(ref GameObject enemy, EnemySO enemySO, Transform spawnPosition)
    {
        enemy.transform.position = spawnPosition.position;

        //asset
        CleanEnemy(enemy);
        GameObject Asset = GameObject.Instantiate(enemySO.tankAsset, enemy.transform);

        //basic Info
        enemy.GetComponent<EnemyController>().CurrentHealth = enemySO.health;
        enemy.GetComponent<EnemyController>().SetIsAlive(true);
        enemy.GetComponent<EnemyController>().Id = enemySO.tankAsset.name;

        //Phisics
        enemy.GetComponent<Rigidbody>().mass = enemySO.mass;
        enemy.GetComponent<BoxCollider>().center = enemySO.boxColliderCenter;
        enemy.GetComponent<BoxCollider>().size = enemySO.boxColliderSize;

        //Weapon
        GameObject cannon = GameObject.Instantiate(enemySO.weapon, enemy.transform);
        cannon.GetComponent<CannonWeapon>().InitialShootPosition = Asset.GetComponent<EnemyComponentFinder>().shootPosition;
        cannon.GetComponent<CannonVFX>().EffectParticle = Asset.GetComponentInChildren<ParticleSystem>();

        //Shoot
        var EnemyShootComponent = enemy.GetComponent<EnemyShoot>();
        EnemyShootComponent.Cannon = cannon.GetComponent<CannonWeapon>();
        EnemyShootComponent.Turret = Asset.GetComponent<EnemyComponentFinder>().turret;
        EnemyShootComponent.ViewRange = enemySO.viewRange;
        EnemyShootComponent.ShootRange = enemySO.shootRange;
        EnemyShootComponent.RotationSpeed = enemySO.rotationSpeed;
    }

    public void NewEnemyConfigure(ref GameObject enemy, EnemySO enemySO, Transform spawnPosition, Transform[] patrolPoints)
    {
        enemy.transform.position = spawnPosition.position;

        //asset
        CleanEnemy(enemy);
        GameObject Asset = GameObject.Instantiate(enemySO.tankAsset, enemy.transform);

        //basic Info
        enemy.GetComponent<EnemyController>().CurrentHealth = enemySO.health;
        enemy.GetComponent<EnemyController>().SetIsAlive(true);
        enemy.GetComponent<EnemyController>().Id = enemySO.tankAsset.name;

        //Phisics
        enemy.GetComponent<Rigidbody>().mass = enemySO.mass;
        enemy.GetComponent<BoxCollider>().center = enemySO.boxColliderCenter;
        enemy.GetComponent<BoxCollider>().size = enemySO.boxColliderSize;

        //Weapon
        GameObject cannon = GameObject.Instantiate(enemySO.weapon, enemy.transform);
        cannon.GetComponent<CannonWeapon>().InitialShootPosition = Asset.GetComponent<EnemyComponentFinder>().shootPosition;
        cannon.GetComponent<CannonVFX>().EffectParticle = Asset.GetComponentInChildren<ParticleSystem>();

        //Shoot
        var EnemyShootComponent = enemy.GetComponent<EnemyShoot>();
        EnemyShootComponent.Cannon = cannon.GetComponent<CannonWeapon>();
        EnemyShootComponent.Turret = Asset.GetComponent<EnemyComponentFinder>().turret;
        EnemyShootComponent.ViewRange = enemySO.viewRange;
        EnemyShootComponent.ShootRange = enemySO.shootRange;
        EnemyShootComponent.RotationSpeed = enemySO.rotationSpeed;

        //Patroll
        enemy.GetComponent<EnemyMove>().patrolPoints = new Transform[patrolPoints.Length];
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            enemy.GetComponent<EnemyMove>().patrolPoints[i] = patrolPoints[i];
        }

        enemy.SetActive(false);
        enemy.SetActive(true);
    }

    private void CleanEnemy(GameObject Enemy)
    {
        if (Enemy.transform.childCount > 0)
        {
            for (int i = 0; i < Enemy.transform.childCount; i++)
            {
                GameObject.Destroy(Enemy.transform.GetChild(i).gameObject);
            }
        }
    }
}
