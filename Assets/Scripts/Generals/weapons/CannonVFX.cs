using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
