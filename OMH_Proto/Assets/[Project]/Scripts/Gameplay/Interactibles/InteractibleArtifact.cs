using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleArtifact : Interactibles
{
    [SerializeField] private GameEvent _getArtifact;

    public override void Interact()
    {
        if (!_isPlayerInRange) return;

        _infosManager.artifact = true;
        
        _getArtifact.Raise();
        
        Destroy(gameObject);
    }
}
