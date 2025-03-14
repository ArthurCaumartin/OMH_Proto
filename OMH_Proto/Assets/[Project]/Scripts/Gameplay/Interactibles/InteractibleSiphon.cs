using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleSiphon : Interactible
{
    [SerializeField] private GameObject _confirmUIStartSiphon;
    [SerializeField] private GameEvent _startDefense;
    
    public override void Interact(PlayerInteract playerInteract, out bool cancelIteraction)
    {
        cancelIteraction = true;
        
        _confirmUIStartSiphon.SetActive(true);
    }

    public void StartSiphon()
    {
        _startDefense.Raise();
        Destroy(gameObject);
    }
}
