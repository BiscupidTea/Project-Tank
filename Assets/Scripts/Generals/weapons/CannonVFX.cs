using UnityEngine;

/// <summary>
/// Cannon Visual effect manager
/// </summary>
public class CannonVFX : MonoBehaviour
{
    [SerializeField] private CannonWeapon cannon;
    [SerializeField] private ParticleSystem effectParticle;

    public ParticleSystem EffectParticle { get => effectParticle; set => effectParticle = value; }

    private void Awake()
    {
        cannon.OnShootCannon += OnAction;
    }

    private void OnDestroy()
    {
        cannon.OnShootCannon -= OnAction;
    }

    private void OnAction()
    {
        if (effectParticle)
        {
            EffectParticle.Play();
        }
    }
}
