using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleGatling : Interactible
{
    [Space]
    [SerializeField] private GameEvent _getGatling;

    public override void Interact(PlayerInteract playerInteract, out bool cancelInteraction)
    {
        cancelInteraction = false;
        
        _getGatling.Raise();
        Destroy(gameObject);
    }
}
