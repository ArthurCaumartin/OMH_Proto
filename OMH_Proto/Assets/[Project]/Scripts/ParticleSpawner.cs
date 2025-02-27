using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleToSpawn;

    public void SpawnParticle()
    {
        ParticleSystem particle = Instantiate(_particleToSpawn, transform);
        Destroy(particle.gameObject, particle.main.duration);
    }
}
