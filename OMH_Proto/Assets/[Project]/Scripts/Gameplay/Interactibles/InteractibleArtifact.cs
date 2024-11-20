using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleArtifact : Interactibles
{
    [SerializeField] private GameEvent _getArtifact;

    public override void Interact()
    {
        if (!_isPlayerInRange) return;

        FloatVariable artifactValue = new FloatVariable();
        
        for (int i = 0; i < _infosManager._variables.Count; i++)
        {
            if (_infosManager._variables[i]._variableName == "Artifact")
            {
                artifactValue = _infosManager._variables[i]._floatVariable;
                break;
            }
        }
        
        artifactValue.Value = 1f;
        
        // print("");
        _getArtifact.Raise();
        
        Destroy(gameObject);
    }
}
