using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GatlingGun : MonoBehaviour
{
    [SerializeField] private FloatReference _gatlingMunitions;
    [SerializeField] private TextMeshProUGUI _gatlingMunitionsText;
    [SerializeField] private Gun _gunScript;
    private float _currentGatlingMunition;

    private void Start()
    {
        _gunScript.GetComponent<Gun>();
        _currentGatlingMunition = _gatlingMunitions.Value;
    }

    public void GatlingShot()
    {
        
        _currentGatlingMunition --;
        _gatlingMunitionsText.text = _currentGatlingMunition.ToString();
        if (_currentGatlingMunition <= 0)
        {
            //Destroy Gatling
        }
    }
}
