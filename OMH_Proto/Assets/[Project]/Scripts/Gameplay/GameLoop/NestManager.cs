using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NestManager : MonoBehaviour
{
    // [SerializeField] private FloatReference _timerStartSpawning, _timerEndSpawning;
    [SerializeField] private GameObject _nestPrefab;
    // [SerializeField] private int _numberNests;

    [SerializeField, Tooltip("Probability of a nest spawning on 100%"), Range(0, 100)] private int _probabilityNestSpawn;
    [SerializeField] private float _timeToTrySpawnNest = 20;
    
    [SerializeField] private List<InteractibleNest> _nestsSpawnPoints = new List<InteractibleNest>();
    private Dictionary<InteractibleNest, bool> _nests = new Dictionary<InteractibleNest, bool>();
    
    // private List<float> _nestTimerSpawn = new List<float>();
    private float _gameTimer;
    private int _spawnerIndex;
    private bool _allSpawned, _isDefenseStarted;

    private void Awake()
    {
        for (int i = 0; i < _nestsSpawnPoints.Count; i++)
        {
            _nests[_nestsSpawnPoints[i]] = false;
        }

        // for (int i = 0; i < _numberNests; i++)
        // {
        //     float tempFloat = Random.Range(_timerStartSpawning.Value, _timerEndSpawning.Value);
        //     _nestTimerSpawn.Add(tempFloat);
        // }
        // _nestTimerSpawn.Sort();
    }

    private void Update()
    {
        if (!_isDefenseStarted) return;
        
        _gameTimer += Time.deltaTime;
        if (_gameTimer >= _timeToTrySpawnNest)
        {
            _gameTimer = 0;
            TrySpawnNest();
        }
        
        // if (_gameTimer >= _nestTimerSpawn[_spawnerIndex])
        // {
        //     _spawnerIndex ++;
        //     SpawnNest();
        //     if (_spawnerIndex >= _numberNests) _allSpawned = true;
        // }
    }

    private void TrySpawnNest()
    {
        int tempInt = Random.Range(0, 99);
        if(tempInt <= _probabilityNestSpawn - 1) SpawnNest();
    }

    private void SpawnNest()
    {
        InteractibleNest tempTransform = new InteractibleNest();
        
        bool isWhileNotFinished = true;
        int randomIndex = 0;
        
        while (isWhileNotFinished)
        {
            randomIndex = Random.Range(0, _nestsSpawnPoints.Count);
            foreach (KeyValuePair<InteractibleNest, bool> kvp in _nests)
            {
                if (kvp.Key == _nestsSpawnPoints[randomIndex])
                {
                    if (kvp.Value == false)
                    {
                        isWhileNotFinished = false;
                    }
                }
            }
        }
        
        foreach (KeyValuePair<InteractibleNest, bool> kvp in _nests)
        {
            if (kvp.Key == _nestsSpawnPoints[randomIndex])
            {
                tempTransform = kvp.Key;
            }
        }
        _nests[tempTransform] = true;
        
        tempTransform.ActivateNest();
    }

    public void DefenseStart()
    {
        _isDefenseStarted = true;
    }
}
