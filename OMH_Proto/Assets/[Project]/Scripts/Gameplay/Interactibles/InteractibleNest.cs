using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractibleNest : Interactible
{
    [Space]
    [SerializeField] private FloatVariable _syringeValue;

    [SerializeField] private GameEvent _destroyNest, _encounterNest, _notEnoughtSyringe;
    [SerializeField] private GameObject _enemyPrefab, _eggMesh, _doorObject1, _doorObject2;
    
    [Space]
    [SerializeField] private Transform _posToSpawn;
    [SerializeField] private float _timeMinSpawn, _timeMaxSpawn;
    [SerializeField] private int _numberOfEnemiesToSpawn;
    [SerializeField] private MobTarget _mobTarget;

    private bool _isNestActive;
    public static bool _isPlayerInRangeForEncounter;
    private float _timerNest, _timerSpawnMob, _timeToSpawn;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.enabled = false;
        
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
        // _syringeValue.Value -= 1;
        _destroyNest.Raise();
        
        if(_eggMesh != null) _eggMesh.SetActive(false);
        Material object1Material = _doorObject1.GetComponent<MeshRenderer>().material;
        object1Material.SetFloat("_infested", 0);
        Material object2Material = _doorObject2.GetComponent<MeshRenderer>().material;
        object2Material.SetFloat("_infested", 0);
        enabled = false;

        _boxCollider.enabled = false;
        Destroy(this);
    }

    public virtual void Update()
    {
        base.Update();
        
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

        _timerNest += Time.deltaTime;
        if (_timerNest > 0.5f)
        {
            _timerNest = 0;
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
            GameObject tempObject = Instantiate(_enemyPrefab, _posToSpawn.position, Quaternion.identity);
            
            tempObject.GetComponent<MobTargetFinder>().Initialize(_mobTarget);
        }
    }

    public void ActivateNest()
    {
        _isNestActive = true;
        _boxCollider.enabled = true;
        
        if(_eggMesh != null) _eggMesh.SetActive(true);
        Material object1Material = _doorObject1.GetComponent<MeshRenderer>().material;
        object1Material.SetFloat("_infested", 1);
        Material object2Material = _doorObject2.GetComponent<MeshRenderer>().material;
        object2Material.SetFloat("_infested", 1);
    }
}
