using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractibleNest : Interactible
{
    [Space]
    [SerializeField] private FloatVariable _syringeValue;

    [SerializeField] private GameEvent _destroyNest, _encounterNest, _notEnoughtSyringe;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _timeMinSpawn, _timeMaxSpawn;
    [SerializeField] private int _numberOfEnemiesToSpawn;

    private bool _isNestActive;
    public static bool _isPlayerInRangeForEncounter;
    private float _timer, _timerSpawnMob, _timeToSpawn;

    private void Awake()
    {
        _timeToSpawn = Random.Range(_timeMinSpawn, _timeMaxSpawn);
        _isPlayerInRangeForEncounter = false;
    }

    public override void Interact(PlayerInteract playerInteract, out bool cancelInteraction)
    {
        cancelInteraction = _syringeValue.Value <= 0;
        if (_syringeValue.Value <= 0) _notEnoughtSyringe.Raise();
    }

    public override void OnQTEWin()
    {
        _syringeValue.Value -= 1;
        _destroyNest.Raise();

        Destroy(gameObject);
    }

    public virtual void Update()
    {
        if (_isNestActive)
        {
            _timerSpawnMob += Time.deltaTime;
            if (_timerSpawnMob >= _timeToSpawn)
            {
                _timeToSpawn = Random.Range(_timeMinSpawn, _timeMaxSpawn);
                _timerSpawnMob = 0;
                SpawnEnemies();
            }
        }

        if (_isPlayerInRangeForEncounter) return;

        _timer += Time.deltaTime;
        if (_timer > 0.5f)
        {
            _timer = 0;
            VerifyPlayerInRangeForText();
        }
    }

    private void VerifyPlayerInRangeForText()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, 5);
        for (int i = 0; i < col.Length; i++)
        {
            if (col[i].CompareTag("Player"))
            {
                _isPlayerInRangeForEncounter = true;
                _encounterNest.Raise();
            }
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < _numberOfEnemiesToSpawn; i++)
        {
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity).GetComponent<StateMachine_Pteramyr>();
        }
    }

    public void ActivateNest()
    {
        _isNestActive = true;
        //Show visuals
    }
}
