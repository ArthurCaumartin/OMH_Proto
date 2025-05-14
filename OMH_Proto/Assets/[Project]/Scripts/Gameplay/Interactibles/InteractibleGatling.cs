using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleGatling : Interactible
{
    [Space]
    [SerializeField] private GameEvent _getGatling;
    [SerializeField] private GameObject _gatlingObject;
    private BoxCollider _collider;

    private new void Start()
    {
        base.Start();
        _collider = GetComponent<BoxCollider>();
    }
    
    public override void Interact(PlayerInteract playerInteract, out bool cancelInteraction)
    {
        cancelInteraction = false;
        
        _getGatling.Raise();
        _gatlingObject.SetActive(false);
        _collider.enabled = false;
    }
}
