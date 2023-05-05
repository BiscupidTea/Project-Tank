using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellLogic : MonoBehaviour
{
    [SerializeField] private GameObject Shell;
    [SerializeField] private Rigidbody Rigidbody;
    [SerializeField] private float explotionForce;
    [SerializeField] private float explotionRadius;

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] entitys = Physics.OverlapSphere(transform.position, explotionRadius);

        foreach (Collider collider in entitys)
        {
            Rigidbody EntRB = collider.GetComponent<Rigidbody>();
            if (EntRB != null)
            {
                EntRB.AddExplosionForce(explotionForce, transform.position, explotionRadius);
                Debug.Log("explotion!");
            }
        }

        Destroy(gameObject);
    }
}
