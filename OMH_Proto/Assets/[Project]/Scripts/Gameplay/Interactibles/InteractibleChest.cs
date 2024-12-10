using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleChest : Interactible
{
    [Space]
    [SerializeField] private GameEvent _openChest;
    [SerializeField] private GameObject _closeChest;
    
    public override void OnQTEWin()
    {
        _openChest.Raise();
        Destroy(_closeChest);
    }

    public override void Update()
    {
        base.Update();
    } 
}
