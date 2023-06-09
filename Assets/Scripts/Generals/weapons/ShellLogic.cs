using UnityEngine;
/// <summary>
/// shell behavior
/// </summary>
public class ShellLogic : MonoBehaviour
{
    [SerializeField] private GameObject Shell;
    [SerializeField] private MeshRenderer ShellRender;
    [SerializeField] private GameObject ExplotionAnimation;
    [SerializeField] private Rigidbody Rigidbody;
    [SerializeField] private float LifeTime;
    [SerializeField] private float explotionForce;
    [SerializeField] private float explotionRadius;
    [SerializeField] private float explotionTimerAnimation;
    [SerializeField] private float damage;
    public float Damage { get => damage; set => damage = value; }

    private float explotionTime = 0;
    private float lifeTimer = 0;
    private bool animationRun;

    private void Start()
    {
        animationRun = false;
        ExplotionAnimation.SetActive(false);
        ShellRender = GetComponentInChildren<MeshRenderer>();
    }

    private void Update()
    {
        lifeTimer += Time.deltaTime;

        if (animationRun)
        {
            explotionTime += Time.deltaTime;
        }

        if (explotionTimerAnimation < explotionTime)
        {
            Destroy(gameObject);
        }

        if (lifeTimer >= LifeTime)
        {
            RunExplotionAnimation();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] entitys = Physics.OverlapSphere(transform.position, explotionRadius);

        foreach (Collider collider in entitys)
        {
            Rigidbody EntRB = collider.GetComponent<Rigidbody>();
            if (EntRB != null)
            {
                EntRB.AddExplosionForce(explotionForce, transform.position, explotionRadius);
                RunExplotionAnimation();
            }
        }

        Health EntityHealth = collision.gameObject.GetComponent<Health>();
        if (EntityHealth != null)
        {
            EntityHealth.ReceiveDamage(damage);
            RunExplotionAnimation();
        }
    }

    private void RunExplotionAnimation()
    {
        Shell.GetComponent<CapsuleCollider>().enabled = false;
        ExplotionAnimation.SetActive(true);
        animationRun = true;
        ShellRender.enabled = false;
        Rigidbody.isKinematic = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explotionRadius);
    }
}
