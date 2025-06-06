using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QTEDoor : Upgradable
{
    [SerializeField] private InteractibleDoor _interactibleDoor;
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
        InitializeDisks();
        
        _startSpeed = _rotationSpeed;
    }

    private void InitializeDisks()
    {
        _diskList.Clear();
        
        //Chose 2 randoms int from 0 to 3. Those numbers are the disks that will not be done in the QTE
        int randomIndex1 = Random.Range(1, 5);
        int randomIndex2 = Random.Range(1, 5);
        while (randomIndex2 == randomIndex1)
        {
            randomIndex2 = Random.Range(0, 4);
        }
        print(randomIndex1 + " : " + randomIndex2);
        if(randomIndex1 == 1 || randomIndex2 == 1) _diskList.Add(_disk1);
        if(randomIndex1 == 2 || randomIndex2 == 2) _diskList.Add(_disk2);
        if(randomIndex1 == 3 || randomIndex2 == 3) _diskList.Add(_disk3);
        if(randomIndex1 == 4 || randomIndex2 == 4) _diskList.Add(_disk4);
        
        
        foreach (RectTransform disk in _diskList)
        {
            float tempRandomZ = Random.Range(0f, 359f);
            disk.transform.localRotation = Quaternion.Euler(0, 0, tempRandomZ);
        }
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

        InitializeDisks();
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
        print("Click disk");
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
        _interactibleDoor.OnQTELose();
        _qte.KillQTE();
    }
}
