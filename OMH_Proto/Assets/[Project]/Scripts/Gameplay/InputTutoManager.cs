using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTutoManager : MonoBehaviour
{
    [SerializeField] private GameObject _tutoCanvas;

    private void Start()
    {
        _tutoCanvas.SetActive(true);
    }

    public void VerifyTutoInputs()
    {
        
    }
}
