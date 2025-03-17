using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStats : MonoBehaviour
{
    [SerializeField] private bool _resetOnStart;
    [SerializeField] private InfosManager _infosManager;

    private void Awake()
    {
        if(!_resetOnStart) return;
        print("ResetStats");
        for (int i = 0; i < _infosManager._variables.Count; i++)
        {
            _infosManager._variables[i].Reset();
        }
    }
}
