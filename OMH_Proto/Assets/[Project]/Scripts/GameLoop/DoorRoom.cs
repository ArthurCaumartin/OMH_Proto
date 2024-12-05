using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRoom : MonoBehaviour
{
    [SerializeField] private AnimatorBoolSetter _animator;
    private bool _objectInRange;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Mob"))
        {
            OpenDoor();
            _objectInRange = true;
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Mob"))
        {
            _objectInRange = false;
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        _animator.SetParametre(true);
    }
    private void CloseDoor()
    {
        _animator.SetParametre(false);
    }
}
