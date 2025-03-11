using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QTEDoor : Upgradable
{
    [SerializeField, Range(0, 500)] private float _rotationSpeed;
    [SerializeField] private Vector3 _axis = Vector3.up;
    [SerializeField] private int _direction = 1;
    
    private bool _isQTEActive;
    private QTE _qte;
    private float _startSpeed;
    
    [Space]
    
    [SerializeField] private RectTransform _disk1, _disk2, _disk3, _disk4;
    private List<RectTransform> _diskList = new List<RectTransform>();
    
    [Space]
    
    [SerializeField] private QTEDoorUI _qteDoorUI;

    private void Start()
    {
        _diskList.Add(_disk1);
        _diskList.Add(_disk2);
        _diskList.Add(_disk3);
        _diskList.Add(_disk4);

        foreach (RectTransform disk in _diskList)
        {
            float tempRandomZ = Random.Range(0f, 359f);
            disk.transform.localRotation = Quaternion.Euler(0, 0, tempRandomZ);
        }
        
        _startSpeed = _rotationSpeed;
    }
    
    public void StartQTE(QTE qteManager)
    {
        _isQTEActive = true;
        _qte = qteManager;
        _qteDoorUI.ActivateUI();
    }
    
    public void ResetQTE()
    {
        _isQTEActive = false;
        _qteDoorUI.ResetUI();
    }

    void Update()
    {
        if (!_isQTEActive) return;

        foreach (RectTransform disk in _diskList)
        {
            disk.transform.Rotate(_axis * (_direction * (Time.deltaTime * _rotationSpeed)));
        }
        
    }

    public void ClickDisk(RectTransform disk)
    {
        if (disk.transform.rotation.eulerAngles.z > 315 || disk.transform.rotation.eulerAngles.z < 45)
        {
            disk.transform.localRotation = Quaternion.Euler(0, 0, 0);
            _diskList.Remove(disk);
            VerifyVictory();
        }
        else
        {
            disk.transform.localRotation = Quaternion.Euler(0, 0, disk.transform.localRotation.eulerAngles.z);
            _qteDoorUI.BadInput();
            StartCoroutine(BadInput());
        }
    }

    private void VerifyVictory()
    {
        if (_diskList.Count == 0)
        {
            _qte.KillQTE();
            _qte.OnWin.Invoke();
        }
    }

    private IEnumerator BadInput()
    {
        _rotationSpeed = 0;
        yield return new WaitForSecondsRealtime(1f);
        _rotationSpeed = _startSpeed;
    }
}
