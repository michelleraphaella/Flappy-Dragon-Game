using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsHelper : MonoBehaviour
{
    public static SpecialEffectsHelper Instance;

    public ParticleSystem fireEffect;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of Special EffectsHelper!");
            return;
        }
        Instance = this;
    }

    private ParticleSystem SpawnParticle(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(prefab, position, Quaternion.identity) as ParticleSystem;
        return newParticleSystem;
    }

    public void Explode(Vector3 position)
    {
        SpawnParticle(fireEffect, position);
    }
}