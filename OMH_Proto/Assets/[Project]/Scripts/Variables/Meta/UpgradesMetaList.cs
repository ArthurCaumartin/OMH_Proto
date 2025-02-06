using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradesList", menuName = "Upgrades List")]
public class UpgradesMetaList : ScriptableObject
{
    public List<UpgradeMeta> _upgrades = new List<UpgradeMeta>();
    public List<UpgradeMeta> _upgradesUnlocked = new List<UpgradeMeta>();
    [Space]
    public List<WeaponMeta> _weapons = new List<WeaponMeta>();
    public List<WeaponMeta> _weaponsUnlocked = new List<WeaponMeta>();
    [Space]
    public List<DefenseMeta> _defenses = new List<DefenseMeta>();
    public List<DefenseMeta> _defensesUnlocked = new List<DefenseMeta>();
}
