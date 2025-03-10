using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEDoor : Upgradable
{
    [SerializeField, Range(50, 500)] private float _rotationSpeed;
    [SerializeField] private Vector3 _axis = Vector3.up;
    [SerializeField] private int _direction = 1;
    private bool _isQTEActive;
    
    [Space]
    
    [SerializeField] private RectTransform _disk1, _disk2, _disk3, _disk4;
    
    void Update()
    {
        // if (!_isQTEActive) return;
        
        _disk1.transform.Rotate(_axis * (_direction * (Time.deltaTime * _rotationSpeed)));
        _disk2.transform.Rotate(_axis * (_direction * (Time.deltaTime * _rotationSpeed)));
        _disk3.transform.Rotate(_axis * (_direction * (Time.deltaTime * _rotationSpeed)));
        _disk4.transform.Rotate(_axis * (_direction * (Time.deltaTime * _rotationSpeed)));
    }

    public void ClickDisk(RectTransform disk)
    {
        if (disk.transform.rotation.eulerAngles.z > -45 && disk.transform.rotation.eulerAngles.z < 45)
        {
            print("Good");
        }
        else
        {
            print("Bad");
        }
    }
}
