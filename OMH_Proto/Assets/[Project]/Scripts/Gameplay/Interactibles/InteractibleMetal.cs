using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleMetal : Interactibles
{
    [SerializeField] private GameEvent _gainMetal;
    
    public override void Interact()
    {
        if (!_isPlayerInRange) return;
        
        FloatVariable metalValue = new FloatVariable();
        
        for (int i = 0; i < _infosManager._variables.Count; i++)
        {
            if (_infosManager._variables[i]._variableName == "Artifact")
            {
                metalValue = _infosManager._variables[i]._floatVariable;
                break;
            }
        }
        
        metalValue.Value += 10;

        _gainMetal.Raise();
        
        Destroy(gameObject);
    }
}
