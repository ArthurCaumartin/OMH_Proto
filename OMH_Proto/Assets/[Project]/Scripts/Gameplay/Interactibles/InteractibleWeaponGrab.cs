using Unity.VisualScripting;
using UnityEngine;

public class InteractibleWeaponGrab : Interactible
{
    [Header("Weapon : ")]
    [SerializeField] private Weapon _weaponTograb;
    [SerializeField] private GameObject _gatlingObject;
    private BoxCollider _collider;

    private void Start()
    {
        base.Start();
        
        _collider = GetComponent<BoxCollider>();
    }
    
    public override void Interact(PlayerInteract playerInteract, out bool cancelIteraction)
    {
        print("try grab weapon");
        cancelIteraction = false;
        playerInteract.GetComponent<WeaponControler>()?.AddWeapon(_weaponTograb);
        
        _gatlingObject.SetActive(false);
        _collider.enabled = false;
    }
}