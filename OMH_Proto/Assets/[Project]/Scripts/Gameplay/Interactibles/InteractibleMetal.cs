using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleMetal : Interactibles
{
    [SerializeField] private GameEvent _gainMetal;
    
    public override void Interact()
    {
        if (!_isPlayerInRange) return;
        
        _infosManager.metal.Value += 10;

        _gainMetal.Raise();
        
        Destroy(gameObject);
    }
}
