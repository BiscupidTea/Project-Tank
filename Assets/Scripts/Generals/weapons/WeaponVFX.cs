using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVFX : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private ParticleSystem effectParticle;
    private void Awake()
    {
        weapon.OnShoot += OnAction;
    }

    private void OnAction(Transform shootOrigin)
    {
        PlayVFX();
    }

    private void PlayVFX()
    {
        effectParticle.Play();
    }
}
