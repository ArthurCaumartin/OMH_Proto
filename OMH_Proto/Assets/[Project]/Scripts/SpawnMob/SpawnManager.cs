using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private MobTarget _gasTankTarget;
    [SerializeField] private FloatReference _explorationDuration;
    
    [SerializeField] private List<WaveParent> _allWavesParents = new List<WaveParent>();
    [SerializeField] private List<EnemySpawner> _spawners = new List<EnemySpawner>();

    [SerializeField] private GameEvent _canStartDefense, _defenseStartEvent;

    [SerializeField] private int _timerMinutesWave1 = 7;
    [SerializeField] private float _timerWaves = 0;
    private int minutes;
    
    private List<WaveParent> _waveParentsToSpawn = new List<WaveParent>();
    
    private float _timerSpawner = 0;
    /// <summary>
    /// Connect to game time
    /// </summary>
    
    public bool _defenseAsStarted, _canStart;
    
    //Assert to verify if there are no errors in Scriptables
    
    private void Start() 
    {
        for (int i = 0; i <  _allWavesParents.Count; i++)
        {
            for (int j = 0; j < _allWavesParents[i]._spawnersInfos.Count; j++)
            {
                _allWavesParents[i]._spawnersInfos[j].hasBeenCalled = false;
            }
        }
        
        for (int i = 0; i < _allWavesParents.Count; i++)
        {
            List<SpawnerScriptable> waves = _allWavesParents[i]._spawnersInfos;
            
            Assert.AreNotEqual(0, waves.Count, "All waves " + i + " need at least 1 spawner");
    
            // if (waves.Count > 2)
            // {
            //     int tempSpawnValue = waves[0].timeToSpawn + waves[0].durationOfSpawn;
            //     for (int y = 1; y < waves.Count; y++)
            //     {
            //         int x = y - 1;
            //         Assert.IsTrue(waves[y].timeToSpawn > tempSpawnValue, "Spawner" + i + " : Wave" + y + " is spawning before Wave" + x + " is finished spawning");
            //         tempSpawnValue = waves[y].timeToSpawn + waves[y].durationOfSpawn;
            //     }
            // }
        }
        
        EnemySpawner[] tempArray = FindObjectsOfType<EnemySpawner>();
        for (int i = 0; i < tempArray.Length; i++)
        {
            _spawners.Add(tempArray[i]);
        }
    
    }
    
    private void Update()
    {
        _timerWaves += Time.deltaTime;
        if (_timerWaves >= 60)
        {
            _timerWaves = 0;
            _timerSpawner = 0;
            minutes++;
            
            if (minutes >= _timerMinutesWave1 && !_canStart)
            {
                _canStartDefense.Raise();
                _canStart = true;
            }

            if (minutes >= _explorationDuration.Value / 60)
            {
                _defenseAsStarted = true;
                _defenseStartEvent.Raise();
            }
        }
        
        if (!_defenseAsStarted) return;
        
        _timerSpawner += Time.deltaTime;
        VerifyIfSpawn();
    }
    
    private void VerifyIfSpawn()
    {
        if (minutes >= 3) return;
        
        for (int j = 0; j < _waveParentsToSpawn[minutes]._spawnersInfos.Count; j++)
        {
            if (_timerSpawner >= _waveParentsToSpawn[minutes]._spawnersInfos[j].timeToSpawn && !_waveParentsToSpawn[minutes]._spawnersInfos[j].hasBeenCalled)
            {
                CallSpawn(_waveParentsToSpawn[minutes]._spawnersInfos[j]._enemiesType, _waveParentsToSpawn[minutes]._spawnersInfos[j].durationOfSpawn, _gasTankTarget,_waveParentsToSpawn[minutes]._spawnersInfos[j]._roomIDToSpawn);

                _waveParentsToSpawn[minutes]._spawnersInfos[j].hasBeenCalled = true;
            }
        }
    }
    
    private void CallSpawn(List<TypesOfEnemies> numberOfMobs, int durationOfSpawn, MobTarget gasTankTarget,int index)
    {
        _spawners[index].SpawnMob(numberOfMobs, durationOfSpawn, gasTankTarget);
    }
    
    public void StartDefense()
    {
        _waveParentsToSpawn.Add(_allWavesParents[minutes - _timerMinutesWave1]);
        _waveParentsToSpawn.Add(_allWavesParents[minutes - _timerMinutesWave1 + 1]);
        _waveParentsToSpawn.Add(_allWavesParents[minutes - _timerMinutesWave1 + 2]);

        _timerWaves = 0;
        minutes = 0;
        
        _defenseAsStarted = true;
    }
}
