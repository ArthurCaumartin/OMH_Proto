using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnim : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private FloatReference _defenseDuration;

    public void Start()
    {
        float tempFloat = (float) ((double) _defenseDuration.Value / 10);
        
        float tempFloat2 = (float) ((double) 1 / tempFloat);
        
        
        _animator.speed = tempFloat2;
    }
}
