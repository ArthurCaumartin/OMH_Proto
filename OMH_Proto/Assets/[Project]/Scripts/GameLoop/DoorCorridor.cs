using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class DoorCorridor : MonoBehaviour, IMapClickable
{
    [SerializeField] private GameObject _doorGameobject;
    [SerializeField] private FloatReference _keyInfos;
    [SerializeField] private GameEvent _updateKey, _navMeshUpdate;
    private SpriteRenderer _spriteRenderer;

    private bool _isOpen;

    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _doorGameobject.SetActive(false);
    }

    public void OnClick()
    {
        if (_isOpen)
        {
            _doorGameobject.SetActive(false);
            _isOpen = false;
            
            _navMeshUpdate.Raise();
            
            _spriteRenderer.color = Color.white;
            
            _keyInfos.Value ++;
            _updateKey.Raise();
            
            print("CloseDoor");
            // Bake le Navmesh
        }
        else
        {
            if (_keyInfos.Value <= 0) return;
            
            _doorGameobject.SetActive(true);
            _isOpen = true;
            
            _navMeshUpdate.Raise();
            
            _spriteRenderer.color = Color.black;
            
            _keyInfos.Value --;
            _updateKey.Raise();
            
            print("OpenDoor");
            // Bake le Navmesh
        }
    }
}
