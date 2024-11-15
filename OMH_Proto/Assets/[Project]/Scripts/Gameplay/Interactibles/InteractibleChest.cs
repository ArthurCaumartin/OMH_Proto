using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleChest : Interactibles
{
    [SerializeField] private GameEvent _openChest;

    public override void Interact()
    {
        if (!_isPlayerInRange) return;
        
        _openChest.Raise();
        
        Destroy(gameObject);
    }
}
