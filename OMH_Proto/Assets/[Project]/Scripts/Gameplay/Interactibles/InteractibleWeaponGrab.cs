using UnityEngine;

public class InteractibleWeaponGrab : Interactible
{
    [Header("Weapon : ")]
    [SerializeField] private Weapon _weaponTograb;

    public override void Interact(PlayerInteract playerInteract, out bool cancelIteraction)
    {
        cancelIteraction = false;
        playerInteract.GetComponent<WeaponControler>()?.AddWeapon(_weaponTograb);
        Destroy(gameObject);
    }
}