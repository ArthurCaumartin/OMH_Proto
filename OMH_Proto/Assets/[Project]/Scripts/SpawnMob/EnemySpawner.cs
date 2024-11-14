using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    
    public void SpawnMob(int numberOfMobs, int durationOfSpawn, MobTarget gasTankTarget)
    {
        GameObject tempObject = Instantiate(_enemyPrefab, transform.position, Quaternion.identity, transform);

        TargetFinder targetFinder = tempObject.GetComponentInChildren<TargetFinder>();
        targetFinder.Initialize(gasTankTarget);
        
        print("Spawn " + numberOfMobs + " enemies during " + durationOfSpawn);
    }
}
