    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleArmory : Interactible
{
    [Space]
    [SerializeField] private GameEvent _onCompleteArmory;
    
    [SerializeField] private GameObject _casierObject;

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
        if (_isArmoryOpened) return;
        _onCompleteArmory.Raise();
        
        gameObject.layer = LayerMask.NameToLayer("Default");
        _casierObject.SetActive(false);

        OpenArmory();
    } 

    public void OpenArmory()
    {
        _isArmoryOpened = true;
    }
}
