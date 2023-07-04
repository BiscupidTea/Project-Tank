using UnityEngine;

/// <summary>
/// Cannon Visual effect manager
/// </summary>
public class CannonVFX : MonoBehaviour
{
    [SerializeField] private CannonWeapon cannon;
    [SerializeField] private ParticleSystem effectParticle;
    private void Awake()
    {
        cannon.OnShootCannon += OnAction;
    }

    private void OnAction()
    {
        effectParticle.Play();
    }
}
