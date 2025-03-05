using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleArmory : Interactible
{
    [Space]
    [SerializeField] private GameEvent _onCompleteArmory;

    // [SerializeField] private Sprite _activatedSprite;

    private bool _isArmoryOpened = false;

    public override void Interact(PlayerInteract playerInteract, out bool cancelIteraction)
    {
        cancelIteraction = _isArmoryOpened;
    }

    public override void OnQTEInput(bool isInputValide)
    {
        
    }

    public override void OnQTEWin()
    {
        // _onCompleteArmory.Raise();
        
        gameObject.layer = LayerMask.NameToLayer("Default");
        // Destroy(gameObject);
        // GetComponent<BoxCollider>().enabled = false;
    } 

    public void OpenArmory()
    {
        // GetComponentInChildren<MapPin>()._tallMapPin = _activatedSprite;
        
        _isArmoryOpened = true;
    }
}
