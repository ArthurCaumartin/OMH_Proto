using UnityEngine;

public class InteractibleWeaponGrab : Interactible
{
    [Header("Weapon : ")]
    [SerializeField] private Weapon _weaponTograb;

    public override void Interact(PlayerInteract playerInteract, out bool cancelIteraction)
    {
        print("try grab weapon");
        cancelIteraction = false;
        playerInteract.GetComponent<WeaponControler>()?.AddWeapon(_weaponTograb);
        Destroy(gameObject);
    }
}