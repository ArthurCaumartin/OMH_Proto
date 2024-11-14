using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<SpawnerScriptable> _spawnerScriptables = new List<SpawnerScriptable>();
    [SerializeField] private List<EnemySpawner> _spawners = new List<EnemySpawner>();

    private float _timer = 0;
    /// <summary>
    /// Connect to game time
    /// </summary>
    
    private bool _defenseAsStarted;
    
    //Assert to verify if there are no errors in Scriptables
    private void Start()
    {
        for (int i = 0; i <  _spawnerScriptables.Count; i++)
        {
            for (int j = 0; j < _spawnerScriptables[i]._waves.Count; j++)
            {
                _spawnerScriptables[i]._waves[j].hasBeenCalled = false;
            }
        }
        
        for (int i = 0; i < _spawnerScriptables.Count; i++)
        {
            List<Wave> waves = _spawnerScriptables[i]._waves;
            
            Assert.AreNotEqual(0, waves.Count, "Spawner " + i + " need at least 1 wave");

            if (waves.Count > 2)
            {
                int tempSpawnValue = waves[0].timeToSpawn + waves[0].durationOfSpawn;
                for (int y = 1; y < waves.Count; y++)
                {
                    int x = y - 1;
                    Assert.IsTrue(waves[y].timeToSpawn > tempSpawnValue, "Spawner" + i + " : Wave" + y + " is spawning before Wave" + x + " is finished spawning");
                    tempSpawnValue = waves[y].timeToSpawn + waves[y].durationOfSpawn;
                }
            }
        }
        
        EnemySpawner[] tempArray = FindObjectsOfType<EnemySpawner>();
        for (int i = 0; i < tempArray.Length; i++)
        {
            _spawners.Add(tempArray[i]);
        }

    }

    private void Update()
    {
        if (!_defenseAsStarted) return;
        
        _timer += Time.deltaTime;
        VerifyIfSpawn();
    }

    private void VerifyIfSpawn()
    {
        for (int i = 0; i < _spawnerScriptables.Count; i++)
        {
            for (int j = 0; j < _spawnerScriptables[i]._waves.Count; j++)
            {
                if (_timer >= _spawnerScriptables[i]._waves[j].timeToSpawn && !_spawnerScriptables[i]._waves[j].hasBeenCalled)
                {
                    CallSpawn(_spawnerScriptables[i]._waves[j].numberOfEnemies, _spawnerScriptables[i]._waves[j].durationOfSpawn, i);

                    _spawnerScriptables[i]._waves[j].hasBeenCalled = true;
                    // _spawnerScriptables[i]._waves.Remove(_spawnerScriptables[i]._waves[j]);
                }
            }
        }
    }

    private void CallSpawn(int numberOfMobs, int durationOfSpawn, int index)
    {
        _spawners[index].SpawnMob(numberOfMobs, durationOfSpawn);
    }
    
    public void StartDefense()
    {
        _defenseAsStarted = true;
    }
}
