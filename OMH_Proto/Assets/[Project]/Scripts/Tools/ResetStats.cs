using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStats : MonoBehaviour
{
    [SerializeField] private InfosManager _infosManager;

    private void Awake()
    {
        print("ResetStats");
        for (int i = 0; i < _infosManager._variables.Count; i++)
        {
            _infosManager._variables[i].Reset();
        }
    }
}
