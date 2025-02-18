using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Chest_Shader_Controller : Interactible
{
    [Space]
    [SerializeField] private GameEvent _openChest;
    [SerializeField] private GameObject _closeChest;
    [SerializeField] private GameObject _mapPin;
    
    public override void OnQTEWin()
    {
        _openChest.Raise();
        Destroy(_closeChest);
        _mapPin.SetActive(false);
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    public override void Update()
    {
        base.Update();
    } 
}
