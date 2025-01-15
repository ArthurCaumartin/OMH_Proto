using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Meta Progression", menuName = "Meta")]
public class MetaProg : ScriptableObject
{
    [SerializeField] private List<WeaponMeta> _weapons = new List<WeaponMeta>();
    [SerializeField] private List<WeaponMeta> _weaponsUnlocked = new List<WeaponMeta>();
    [SerializeField] private WeaponMeta _weaponChoose;
    
    [SerializeField] private List<DefenseMeta> _defenses = new List<DefenseMeta>();
    [SerializeField] private List<DefenseMeta> _defensesUnlocked = new List<DefenseMeta>();
    [SerializeField] private WeaponMeta _defenseChoose;
}
