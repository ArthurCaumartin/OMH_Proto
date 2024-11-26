using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleKey : Interactible
{
    [Space]
    [SerializeField] private GameEvent _getKey;
    [SerializeField] private FloatVariable keyValue;

    public override void Interact(out bool cancelInteraction)
    {
        cancelInteraction = false;
        
        keyValue.Value += 1f;
        _getKey.Raise();
        Destroy(gameObject);
    }
}
