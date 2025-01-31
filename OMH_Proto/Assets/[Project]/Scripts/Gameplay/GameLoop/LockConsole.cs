using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class LockConsole : Interactible
{
    [SerializeField] private GameObject _doorGameobject, _mapPinLock;
    [SerializeField] private FloatReference _keyInfos;
    [SerializeField] private GameEvent _updateKey, _navMeshUpdate;

    public void Awake()
    {
        _doorGameobject.SetActive(false);
    }
    
    public override void Interact(out bool canelInteraction)
    {
        canelInteraction = false;
        
        if (_keyInfos.Value <= 0) return;
        
        _doorGameobject.SetActive(true);
        _mapPinLock.SetActive(true);
        
        _navMeshUpdate.Raise();
        
        _keyInfos.Value --;
        
        Destroy(gameObject);
    }
}
