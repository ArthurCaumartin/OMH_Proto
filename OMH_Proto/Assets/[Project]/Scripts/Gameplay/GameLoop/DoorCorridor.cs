using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class DoorCorridor : MonoBehaviour, IMapClickable
{
    [SerializeField] private GameObject _doorGameobject, _mapPinLock;
    [SerializeField] private FloatReference _keyInfos;
    [SerializeField] private GameEvent _updateKey, _navMeshUpdate;

    private bool _isOpen;

    public void Start()
    {
        _doorGameobject.SetActive(false);
    }

    public void OnClick()
    {
        if (_isOpen)
        {
            _doorGameobject.SetActive(false);
            _isOpen = false;
            
            _navMeshUpdate.Raise();
            
            _mapPinLock.SetActive(false);
            
            _keyInfos.Value ++;
        }
        else
        {
            if (_keyInfos.Value <= 0) return;
            
            _doorGameobject.SetActive(true);
            _isOpen = true;
            
            _navMeshUpdate.Raise();
            
            _mapPinLock.SetActive(true);
            
            _keyInfos.Value --;
        }
    }
}
