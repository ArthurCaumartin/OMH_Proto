using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleChest : Interactible
{
    [Space]
    [SerializeField] private GameEvent _openChest;
    
    public override void OnQTEWin()
    {
        _openChest.Raise();
        Destroy(gameObject);
    }

    public override void Update()
    {
        base.Update();
    } 
}
