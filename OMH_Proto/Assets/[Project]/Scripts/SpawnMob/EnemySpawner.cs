using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    private float _numberEnemies;
    private bool _isNestDestroyed;
    
    public void SpawnMob(int numberOfMobs, int durationOfSpawn, MobTarget gasTankTarget)
    {
        _numberEnemies = numberOfMobs;
        _numberEnemies = Mathf.RoundToInt(_numberEnemies / 2);
        
        GameObject tempObject = Instantiate(_enemyPrefab, transform.position, Quaternion.identity, transform);

        TargetFinder targetFinder = tempObject.GetComponentInChildren<TargetFinder>();
        targetFinder.Initialize(gasTankTarget);
        
        print("Spawn " + numberOfMobs + " enemies during " + durationOfSpawn);
    }

    public void DestroyNest()
    {
        _isNestDestroyed = true;
    }
}
