using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryShoot : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float delayBetweenSheels;
    [SerializeField] private int cuantity;
    [SerializeField] private int attackHeight;
    [SerializeField] private GameObject ArtilleryShell;
    private int attackRange;
    private Vector3 FirstPosition;
    private List<GameObject> shellsList = new List<GameObject>();

    public int AttackRange { get => attackRange; set => attackRange = value; }

    void Start()
    {
        FirstPosition = gameObject.transform.position;
        FirstPosition.y += attackHeight;

        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        for (int i = 0; i < cuantity; i++)
        {
            yield return new WaitForSeconds(delayBetweenSheels);
            SpawnShell();
        }
    }

    private void SpawnShell()
    {
        Vector3 offSet = new Vector3(Random.Range(0, attackRange), 0, Random.Range(0, attackRange));
        Vector3 spawnPoint = FirstPosition + offSet;
        Quaternion spawnRotation = Quaternion.LookRotation(Vector3.down);

        GameObject NewSheel = Instantiate(ArtilleryShell, spawnPoint, spawnRotation, transform);
        NewSheel.GetComponent<ShellLogic>().LifeTime = 100;
        NewSheel.GetComponent<ShellLogic>().ExplotionRadius = NewSheel.GetComponent<ShellLogic>().ExplotionRadius * 3;

        shellsList.Add(NewSheel);
    }

    public void RemoveShellFromList(GameObject shell)
    {
        shellsList.Remove(shell);

        if (shellsList.Count <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, AttackRange);

        Gizmos.DrawWireSphere(FirstPosition, 1);
    }
}
