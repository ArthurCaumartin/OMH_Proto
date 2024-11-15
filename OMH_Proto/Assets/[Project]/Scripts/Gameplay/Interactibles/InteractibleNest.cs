using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleNest : Interactibles
{
    [SerializeField] private GameEvent _destroyNest;

    [SerializeField] private GameObject _enemySpawner;

    public override void Interact()
    {
        if (!_isPlayerInRange) return;

        if (_infosManager.syringe.Value <= 0)
        {
            Debug.Log("Dont have enough SERINGE");
            return;
        }

        _infosManager.syringe.Value -= 1;
        _destroyNest.Raise();
        
        _enemySpawner.BroadcastMessage("DestroyNest");
        
        Destroy(gameObject);
    }
}
