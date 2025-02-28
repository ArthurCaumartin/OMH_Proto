using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRoom : MonoBehaviour
{
    [SerializeField] private AnimatorBoolSetter _animator;
    private bool _objectInRange;
    private List<GameObject> _doors = new List<GameObject>();
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Mob"))
        {
            if (_doors.Count == 0)
            {
                OpenDoor();
                _objectInRange = true;
            }
            _doors.Add(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Mob"))
        {
            _doors.Remove(other.gameObject);
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

    private void SpawnParticle()
    {
        foreach (var item in GetComponentsInChildren<ParticleSpawner>())
            item?.SpawnParticle();
    }
}
