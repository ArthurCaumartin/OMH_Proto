using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleArtifact : Interactible
{
    [Space]
    [SerializeField] private FloatVariable _artifactValue;
    [SerializeField] private GameEvent _getArtifact;

    public override void Interact(out bool canelInteraction)
    {
        canelInteraction = false;
        
        _artifactValue.Value = 1f;
        _getArtifact.Raise();
        Destroy(gameObject);
    }
}
