using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _mapPin;

    private MobTarget _mobTarget;
    private List<float> _timerSpawnEnemies = new List<float>();
    private List<GameObject> _enemiesPrefabs = new List<GameObject>();
    
    private float _timer;
    private bool _isNestDestroyed, _spawnEnemies;

    private int tempInt, _instantiatedEnemiesIndex;
    
    private List<TypeEnemiesStruct> _spawnedEnemies = new List<TypeEnemiesStruct>();

    public class TypeEnemiesStruct
    {
        public GameObject typeOfEnemy;
        public int numberOfEnemies;

        public TypeEnemiesStruct(GameObject prefab, int numbers)
        {
            typeOfEnemy = prefab;
            numberOfEnemies = numbers;
        }
    }

    private void Update()
    {
        VerifySpawnEnemy();
    }

    private void VerifySpawnEnemy()
    {
        if (_spawnEnemies)
        {
            _timer += Time.deltaTime;

            if (_timer >= _timerSpawnEnemies[0])
            {
                InstantiateEnemy();
                _timerSpawnEnemies.RemoveAt(0);
                _instantiatedEnemiesIndex++;
                if (_timerSpawnEnemies.Count <= 0)
                {
                    _spawnEnemies = false;
                    _timer = 0;
                }
            }
        }
    }

    public void SpawnMob(List<TypesOfEnemies> numberOfMobs, float durationOfSpawn, MobTarget gasTankTarget)
    {
        _instantiatedEnemiesIndex = 0;
        _mobTarget = gasTankTarget;

        foreach (TypesOfEnemies mob in numberOfMobs)
        {
            _spawnedEnemies.Add(new TypeEnemiesStruct(mob._mobPrefab, mob._mobNumber));
        }

        tempInt = 0;
        for (int i = 0; i < _spawnedEnemies.Count; i++)
        {
            tempInt += _spawnedEnemies[i].numberOfEnemies;
        }
        print("Spawn " + tempInt + " enemies during " + durationOfSpawn);
        
        for (int i = 0; i < tempInt; i++)
        {
            float tempFloat = Random.Range(0f, durationOfSpawn);
            _timerSpawnEnemies.Add(tempFloat);
            
            //Add random enemy
            bool tempBool = true;
            while (tempBool)
            {
                int tempRandomInt = Random.Range(0, _spawnedEnemies.Count);
                if (_spawnedEnemies[tempRandomInt].numberOfEnemies > 0)
                {
                    _enemiesPrefabs.Add(_spawnedEnemies[tempRandomInt].typeOfEnemy);
                    _spawnedEnemies[tempRandomInt].numberOfEnemies --;
                    tempBool = false;
                }
            }
        }
        _timerSpawnEnemies.Sort();
        
        _spawnEnemies = true;
    }

    private void InstantiateEnemy()
    {
        float tempZPos = Random.Range(-5f, 5f);
        float tempXPos = Random.Range(-5f, 5f);

        Vector3 posToSpawnEnemy = new Vector3(transform.position.x + tempXPos, transform.position.y, transform.position.z + tempZPos);
        
        
        GameObject tempObject = Instantiate(_enemiesPrefabs[_instantiatedEnemiesIndex], posToSpawnEnemy, Quaternion.identity, transform);
        
        // tempObject.BroadcastMessage("Initialize", gasTankTarget);
        MobTargetFinder agentTargetFinder = tempObject.GetComponentInChildren<MobTargetFinder>();
        agentTargetFinder.Initialize(_mobTarget);
    }

    public void DestroyNest()
    {
        _isNestDestroyed = true;
        print("Spawner nerfed");
    }
}
