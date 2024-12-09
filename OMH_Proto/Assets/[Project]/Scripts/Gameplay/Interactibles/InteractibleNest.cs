using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleNest : Interactible
{
    [Space]
    [SerializeField] private FloatVariable _syringeValue;

    [SerializeField] private GameEvent _destroyNest, _encounterNest, _notEnoughtSyringe;
    [SerializeField] private GameObject _enemySpawner;
    
    public static bool _isPlayerInRangeForEncounter;
    private float _timer;

    private void Awake()
    {
        _isPlayerInRangeForEncounter = false;
    }

    public override void Interact(out bool cancelInteraction)
    {
        cancelInteraction = _syringeValue.Value <= 0;
        _notEnoughtSyringe.Raise();
    }

    public override void OnQTEWin()
    {
        _syringeValue.Value -= 1;
        _destroyNest.Raise();

        if (_enemySpawner) _enemySpawner.BroadcastMessage("DestroyNest");

        Destroy(gameObject);
    }
    
    public virtual void Update()
    {
        base.Update();
        
        if (_isPlayerInRangeForEncounter) return;
        
        _timer += Time.deltaTime;
        if (_timer > 0.5f)
        {
            _detectionTime = 0;
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
}
