using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradesList", menuName = "Upgrades List")]
public class UpgradesList : ScriptableObject
{
    public List<UpgradeMeta> upgrades = new List<UpgradeMeta>();
    public List<UpgradeMeta> upgradesUnlocked = new List<UpgradeMeta>();
    public List<UpgradeMeta> upgradesChooseGame = new List<UpgradeMeta>(); 
}
