using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStats : MonoBehaviour
{
    [SerializeField] private InfosManager _infosManager;
    [SerializeField] private float _baseMetal, _baseSyringe, _baseKey;
    [SerializeField] private bool _baseArtifact;

    private void Start()
    {
        _infosManager.metal.Value = _baseMetal;
        _infosManager.syringe.Value = _baseSyringe;
        _infosManager.key.Value = _baseKey;
        _infosManager.artifact = false;
    }
}
