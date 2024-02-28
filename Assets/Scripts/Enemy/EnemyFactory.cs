using UnityEngine;

public class EnemyFactory
{

    public void NewEnemyTankConfigure(ref GameObject enemy, EnemyTankSO enemySO, Transform spawnPosition)
    {
        enemy.transform.position = spawnPosition.position;
        enemy.transform.rotation = spawnPosition.rotation;

        //asset
        CleanEnemy(enemy);
        GameObject Asset = GameObject.Instantiate(enemySO.Asset, enemy.transform);

        //basic Info
        enemy.GetComponent<EnemyTankController>().CurrentHealth = enemySO.health;
        enemy.GetComponent<EnemyTankController>().SetIsAlive(true);
        enemy.GetComponent<EnemyTankController>().Id = enemySO.Asset.name;

        //Phisics
        enemy.GetComponent<Rigidbody>().mass = enemySO.mass;
        enemy.GetComponent<BoxCollider>().center = enemySO.boxColliderCenter;
        enemy.GetComponent<BoxCollider>().size = enemySO.boxColliderSize;

        //Weapon
        GameObject cannon = GameObject.Instantiate(enemySO.weapon, enemy.transform);
        cannon.GetComponent<CannonWeapon>().InitialShootPosition = Asset.GetComponent<EnemyComponentFinder>().shootPosition;
        cannon.GetComponent<CannonWeapon>().Damage = enemySO.damage;
        cannon.GetComponent<CannonWeapon>().Force = enemySO.force;
        cannon.GetComponent<CannonVFX>().EffectParticle = Asset.GetComponentInChildren<ParticleSystem>();

        //Shoot
        var EnemyShootComponent = enemy.GetComponent<EnemyTankShoot>();
        EnemyShootComponent.Cannon = cannon.GetComponent<CannonWeapon>();
        EnemyShootComponent.AssetTurret = Asset.GetComponent<EnemyComponentFinder>().turret;
        EnemyShootComponent.AssetCannon = Asset.GetComponent<EnemyComponentFinder>().cannon;

        EnemyShootComponent.ViewRange = enemySO.viewRange;
        EnemyShootComponent.ShootRange = enemySO.shootRange;

        EnemyShootComponent.TurretRotationSpeed = enemySO.turretRotationSpeed;
        EnemyShootComponent.CannonRotationSpeed = enemySO.cannonRotationSpeed;
        EnemyShootComponent.CannonMaxRotation = enemySO.cannonMaxRotation;
        EnemyShootComponent.CannonMinRotation = enemySO.cannonMinRotation;
    }

    public void NewEnemyTankConfigure(ref GameObject enemy, EnemyTankSO enemySO, Transform spawnPosition, Transform[] patrolPoints)
    {
        enemy.transform.position = spawnPosition.position;

        //asset
        CleanEnemy(enemy);
        GameObject Asset = GameObject.Instantiate(enemySO.Asset, enemy.transform);

        //basic Info
        enemy.GetComponent<EnemyTankController>().CurrentHealth = enemySO.health;
        enemy.GetComponent<EnemyTankController>().SetIsAlive(true);
        enemy.GetComponent<EnemyTankController>().Id = enemySO.Asset.name;

        //Phisics
        enemy.GetComponent<Rigidbody>().mass = enemySO.mass;
        enemy.GetComponent<BoxCollider>().center = enemySO.boxColliderCenter;
        enemy.GetComponent<BoxCollider>().size = enemySO.boxColliderSize;

        //Weapon
        GameObject cannon = GameObject.Instantiate(enemySO.weapon, enemy.transform);
        cannon.GetComponent<CannonWeapon>().InitialShootPosition = Asset.GetComponent<EnemyComponentFinder>().shootPosition;
        cannon.GetComponent<CannonWeapon>().Damage = enemySO.damage;
        cannon.GetComponent<CannonWeapon>().Force = enemySO.force;
        cannon.GetComponent<CannonVFX>().EffectParticle = Asset.GetComponentInChildren<ParticleSystem>();

        //Shoot
        var EnemyShootComponent = enemy.GetComponent<EnemyTankShoot>();
        EnemyShootComponent.Cannon = cannon.GetComponent<CannonWeapon>();
        EnemyShootComponent.AssetTurret = Asset.GetComponent<EnemyComponentFinder>().turret;
        EnemyShootComponent.AssetCannon = Asset.GetComponent<EnemyComponentFinder>().cannon;

        EnemyShootComponent.ViewRange = enemySO.viewRange;
        EnemyShootComponent.ShootRange = enemySO.shootRange;

        EnemyShootComponent.TurretRotationSpeed = enemySO.turretRotationSpeed;
        EnemyShootComponent.CannonRotationSpeed = enemySO.cannonRotationSpeed;
        EnemyShootComponent.CannonMaxRotation = enemySO.cannonMaxRotation;
        EnemyShootComponent.CannonMinRotation = enemySO.cannonMinRotation;

        //Patroll
        enemy.GetComponent<EnemyTankMovement>().patrolPoints = new Transform[patrolPoints.Length];
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            enemy.GetComponent<EnemyTankMovement>().patrolPoints[i] = patrolPoints[i];
        }

        enemy.SetActive(false);
        enemy.SetActive(true);
    }

    public void NewEnemyPlaneConfigure(ref GameObject enemy, EnemyPlaneSO enemySO, Transform spawnPosition, Transform[] patrolPoints)
    {
        enemy.transform.position = spawnPosition.position;

        //asset
        CleanEnemy(enemy);
        GameObject Asset = GameObject.Instantiate(enemySO.Asset, enemy.transform);

        //basic Info
        var EnemyControllerComponent = enemy.GetComponent<EnemyPlaneController>();
        EnemyControllerComponent.CurrentHealth = enemySO.health;
        EnemyControllerComponent.SetIsAlive(true);
        EnemyControllerComponent.Id = enemySO.Asset.name;

        //movement
        var EnemyMoveComponent = enemy.GetComponent<EnemyPlaneMovement>();
        EnemyControllerComponent.FlyEnemyMovement = EnemyMoveComponent;
        EnemyMoveComponent.MinDistancePatrolPoints = enemySO.minDistancePatrolPoints;
        EnemyMoveComponent.MinDistancePlayer = enemySO.minDistancePlayer;
        EnemyMoveComponent.MovementSpeed = enemySO.movementSpeed;
        EnemyMoveComponent.RotationSpeed = enemySO.rotationSpeed;

        //Phisics
        enemy.GetComponent<BoxCollider>().center = enemySO.boxColliderCenter;
        enemy.GetComponent<BoxCollider>().size = enemySO.boxColliderSize;

        //Weapon
        GameObject cannon = GameObject.Instantiate(enemySO.weapon, enemy.transform);
        cannon.GetComponent<CannonWeapon>().InitialShootPosition = Asset.GetComponent<EnemyComponentFinder>().shootPosition;
        cannon.GetComponent<CannonWeapon>().Damage = enemySO.damage;

        //Shoot
        var EnemyShootComponent = enemy.GetComponent<EnemyPlaneShoot>();
        EnemyControllerComponent.FlyEnemyAttack = EnemyShootComponent;
        EnemyShootComponent.Cannon = cannon.GetComponent<CannonWeapon>();
        EnemyShootComponent.ViewRange = enemySO.viewRange;
        EnemyShootComponent.AttackDelay = enemySO.attackDelay;
        EnemyShootComponent.AttackPlayerDistance = enemySO.attackPlayerDistance;
        EnemyShootComponent.ShootPosition = Asset.GetComponent<EnemyComponentFinder>().shootPosition;

        //Patroll
        EnemyMoveComponent.PatrolPoints = new Transform[patrolPoints.Length];
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            EnemyMoveComponent.PatrolPoints[i] = patrolPoints[i];
        }

        EnemyControllerComponent.AddListenersToController();
        EnemyMoveComponent.StartPlaneBasics();
        EnemyShootComponent.StartPlaneBasics();
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
