using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStats : MonoBehaviour
{
    [SerializeField] private bool _resetOnStart;
    [SerializeField] private InfosManager _infosManager;
    [SerializeField] private FloatVariable _gameTime;
    private void Awake()
    {
        if(!_resetOnStart) return;
        print("ResetStats");
        for (int i = 0; i < _infosManager._variables.Count; i++)
        {
            _infosManager._variables[i].Reset();
        }

        _gameTime.Value = 0;
    }

    private void Update()
    {
        _gameTime.Value += Time.deltaTime;
    }
}
