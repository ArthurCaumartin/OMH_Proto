using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorRoom : MonoBehaviour
{
    [SerializeField] private AnimatorBoolSetter _animator;
    [SerializeField] private NavMeshObstacle _doorNavMeshObstacle;
    [SerializeField] private GameObject _lockedDoorVisual, _tempSealedDoorVisual;
    [SerializeField] private float _doorLockTime = 60f;
    [SerializeField] private GameEvent _navMeshUpdate;
    
    private bool _objectInRange;
    private List<GameObject> _doors = new List<GameObject>();

    private bool _isDoorLocked;
    private float _verifTimer;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Mob"))
        {
            _doors.Add(other.gameObject);
            
            if(_isDoorLocked) return;
            
            if (_doors.Count == 1)
            {
                OpenDoor();
                _objectInRange = true;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player") || other.CompareTag("Mob"))
        {
            _doors.Remove(other.gameObject);
            
            if(_isDoorLocked) return;
            
            if (_doors.Count == 0)
            {
                _objectInRange = false;
                CloseDoor();
            }
        }
    }

    private void Update()
    {
        _verifTimer += Time.deltaTime;

        if (_verifTimer >= 2)
        {
            _verifTimer = 0;
            for (int i = 0; i < _doors.Count; i++)
            {
                if(_doors[i] == null) _doors.RemoveAt(i);
            }
            
            if (_doors.Count == 0)
            {
                _objectInRange = false;
                CloseDoor();
            }
        }
    }

    private void OpenDoor()
    {
        _animator.SetParametre(true);
        SpawnParticle();
    }
    private void CloseDoor()
    {
        _animator.SetParametre(false);
    }

    public void LockDoor()
    {
        _isDoorLocked = true;
        _lockedDoorVisual.SetActive(true);
        
        _doorNavMeshObstacle.carving = true;
        _navMeshUpdate.Raise();
    }

    public void UnlockDoor()
    {
        _isDoorLocked = false;
        _lockedDoorVisual.SetActive(false);
        _tempSealedDoorVisual.SetActive(false);
        
        _doorNavMeshObstacle.carving = false;
        _navMeshUpdate.Raise();

        OpenDoor();
    }

    public void TempSealDoor()
    {
        _lockedDoorVisual.SetActive(false);
        _tempSealedDoorVisual.SetActive(true);
        
        StartCoroutine(TempLockDoor());
    }

    private void SpawnParticle()
    {
        foreach (var item in GetComponentsInChildren<ParticleSpawner>())
            item?.SpawnParticle();
    }

    private IEnumerator TempLockDoor()
    {
        yield return new WaitForSeconds(_doorLockTime);
        UnlockDoor();
    }
}
