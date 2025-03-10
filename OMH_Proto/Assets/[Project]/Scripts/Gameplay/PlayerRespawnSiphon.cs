using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnSiphon : MonoBehaviour
{
    [SerializeField] private Transform _posToRespawn;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Shield playerShield = other.GetComponent<Shield>();
            playerShield.SetRespawnPos(_posToRespawn.position);
            
            Destroy(gameObject);
        }
    }
}
