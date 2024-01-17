using UnityEngine;

public class ArtilleryWeapon : Weapon
{
    [SerializeField] private int attackRange;
    [SerializeField] private GameObject PrefabArtilleryShoot;
    public override void Shoot()
    {
        if (TryConsumeAmmo(1))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("spawn ArtilleryShoot");
                GameObject NewArtilleryAttack = Instantiate(PrefabArtilleryShoot, hit.point, Quaternion.identity);
                NewArtilleryAttack.GetComponent<ArtilleryShoot>().AttackRange = attackRange;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(InitialShootPosition.position, 1);

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * 300);
    }
}
