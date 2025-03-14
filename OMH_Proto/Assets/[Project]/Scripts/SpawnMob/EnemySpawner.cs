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

    private TypesOfEnemies[] _numberEnemies;
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

            if (_timer >= _timerSpawnEnemies[0])
            {
                InstantiateEnemy();
                _timerSpawnEnemies.RemoveAt(0);
                _enemiesPrefabs.RemoveAt(0);
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
        _mobTarget = gasTankTarget;
        
        _numberEnemies = new TypesOfEnemies[numberOfMobs.Count];
        numberOfMobs.CopyTo(_numberEnemies);
        
        // if (_isNestDestroyed)
        // {
        //     _numberEnemies = Mathf.RoundToInt(_numberEnemies / 2);
        // }
        
        print("Spawn " + _numberEnemies + " enemies during " + durationOfSpawn);

        int tempInt = 0;
        for (int i = 0; i < _numberEnemies.Length; i++)
        {
            tempInt += _numberEnemies[i]._mobNumber;
        }
        
        for (int i = 0; i < tempInt; i++)
        {
            float tempFloat = Random.Range(0f, durationOfSpawn);
            _timerSpawnEnemies.Add(tempFloat);
            
            //Add random enemy
            bool tempBool = true;
            while (tempBool)
            {
                tempInt = Random.Range(0, _numberEnemies.Length);
                if (_numberEnemies[tempInt]._mobNumber > 0)
                {
                    _enemiesPrefabs.Add(_numberEnemies[tempInt]._mobPrefab);
                    _numberEnemies[tempInt]._mobNumber --;
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
        
        
        GameObject tempObject = Instantiate(_enemiesPrefabs[0], posToSpawnEnemy, Quaternion.identity, transform);
        
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
