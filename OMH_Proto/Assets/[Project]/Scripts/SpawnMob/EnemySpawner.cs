using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    private MobTarget _mobTarget;
    private List<float> timerSpawnEnemies = new List<float>();

    private float _numberEnemies;
    private float _timer;
    private bool _isNestDestroyed, _spawnEnemies;

    private void Update()
    {
        VerifySpawnEnemy();
    }

    private void VerifySpawnEnemy()
    {
        if (_spawnEnemies)
        {
            _timer += Time.deltaTime;

            if (_timer >= timerSpawnEnemies[0])
            {
                InstantiateEnemy();
                timerSpawnEnemies.RemoveAt(0);
                if (timerSpawnEnemies.Count <= 0)
                {
                    _spawnEnemies = false;
                    _timer = 0;
                }
            }
        }
    }

    public void SpawnMob(int numberOfMobs, float durationOfSpawn, MobTarget gasTankTarget)
    {
        _spawnEnemies = true;
        _mobTarget = gasTankTarget;
        _numberEnemies = numberOfMobs;
        
        if (_isNestDestroyed)
        {
            _numberEnemies = Mathf.RoundToInt(_numberEnemies / 2);
        }
        
        print("Spawn " + _numberEnemies + " enemies during " + durationOfSpawn);
        
        for (int i = 0; i < _numberEnemies; i++)
        {
            float tempFloat = Random.Range(0f, durationOfSpawn);
            timerSpawnEnemies.Add(tempFloat);
        }
        timerSpawnEnemies.Sort();
    }

    private void InstantiateEnemy()
    {
        float tempZPos = Random.Range(-5f, 5f);
        float tempXPos = Random.Range(-5f, 5f);

        Vector3 posToSpawnEnemy = new Vector3(transform.position.x + tempXPos, transform.position.y, transform.position.z + tempZPos);
        
        
        GameObject tempObject = Instantiate(_enemyPrefab, posToSpawnEnemy, Quaternion.identity, transform);
        
        // tempObject.BroadcastMessage("Initialize", gasTankTarget);
        AgentTargetFinder agentTargetFinder = tempObject.GetComponentInChildren<AgentTargetFinder>();
        agentTargetFinder.Initialize(_mobTarget);
    }

    public void DestroyNest()
    {
        _isNestDestroyed = true;
        print("Spawner nerfed");
    }
}
