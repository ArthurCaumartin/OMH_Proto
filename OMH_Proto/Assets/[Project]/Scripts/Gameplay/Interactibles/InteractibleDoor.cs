using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleDoor : Interactible
{
    
    [Space]
    [SerializeField] private GameEvent _onOpenDoor, _onLockDoor;

    [SerializeField] private Sprite _activatedSprite;
    
    private float _timer;
    public bool _isDoorLocked;

    public override void Interact(PlayerInteract playerInteract, out bool cancelIteraction)
    {
        cancelIteraction = _isDoorLocked;
    }

    public override void OnQTEInput(bool isInputValide)
    {
        
    }

    public override void OnQTEWin()
    {
        _isDoorLocked = false;
        _onOpenDoor.Raise();

        
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public override void OnQTEKill()
    {
        _isDoorLocked = true;
        _onLockDoor.Raise();
    }

    private void Update()
    {
        
    }

    public void ActivateGenerator()
    {
        GetComponentInChildren<MapPin>()._tallMapPin = _activatedSprite;
        
        _isDoorLocked = true;
    }
}
