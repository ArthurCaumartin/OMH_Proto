using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    
    public void SpawnMob(int numberOfMobs, int durationOfSpawn)
    {
        GameObject tempObject = Instantiate(_enemyPrefab, Vector3.zero, Quaternion.identity, transform);
            
            
        print("Spawn " + numberOfMobs + " enemies during " + durationOfSpawn);
    }
}
