using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradesList", menuName = "Upgrades List")]
public class UpgradesList : ScriptableObject
{
    public List<UpgradeMeta> _upgrades = new List<UpgradeMeta>();
    public List<UpgradeMeta> _upgradesUnlocked = new List<UpgradeMeta>();
    public List<UpgradeMeta> _upgradesChooseGame = new List<UpgradeMeta>();
}
