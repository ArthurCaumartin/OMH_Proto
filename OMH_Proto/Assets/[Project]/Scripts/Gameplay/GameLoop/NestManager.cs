using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NestManager : MonoBehaviour
{
    [SerializeField] private FloatReference _timerStartSpawning, _timerEndSpawning;
    [SerializeField] private GameObject _nestPrefab;
    [SerializeField] private int _numberNests;
    
    [SerializeField] private List<Transform> _nestsSpawnPoints = new List<Transform>();
    private Dictionary<Transform, bool> _nests = new Dictionary<Transform, bool>();
    
    private List<float> _nestTimerSpawn = new List<float>();
    private float _gameTimer;
    private int _spawnerIndex;
    private bool _allSpawned;

    private void Awake()
    {
        for (int i = 0; i < _nestsSpawnPoints.Count; i++)
        {
            _nests[_nestsSpawnPoints[i]] = false;
        }

        for (int i = 0; i < _numberNests; i++)
        {
            float tempFloat = Random.Range(_timerStartSpawning.Value, _timerEndSpawning.Value);
            _nestTimerSpawn.Add(tempFloat);
        }
        _nestTimerSpawn.Sort();
    }

    private void Update()
    {
        if (_allSpawned) return;
        
        _gameTimer += Time.deltaTime;
        if (_gameTimer >= _nestTimerSpawn[_spawnerIndex])
        {
            _spawnerIndex ++;
            SpawnNest();
            if (_spawnerIndex >= _numberNests) _allSpawned = true;
        }
    }

    private void SpawnNest()
    {
        Transform tempTransform = transform;
        
        bool isWhileNotFinished = true;
        int randomIndex = 0;
        
        while (isWhileNotFinished)
        {
            randomIndex = Random.Range(0, _nestsSpawnPoints.Count);
            foreach (KeyValuePair<Transform, bool> kvp in _nests)
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
        
        foreach (KeyValuePair<Transform, bool> kvp in _nests)
        {
            if (kvp.Key == _nestsSpawnPoints[randomIndex])
            {
                tempTransform = kvp.Key;
            }
        }
        _nests[tempTransform] = true;
        
        GameObject tempGameObject = Instantiate(_nestPrefab, tempTransform);
    }
}
