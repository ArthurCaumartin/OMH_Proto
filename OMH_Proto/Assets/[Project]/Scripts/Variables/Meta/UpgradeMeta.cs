using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade Meta")]
public class UpgradeMeta : ScriptableObject
{
    public int _upgradeCost;
    [TextArea] public string _upgradeDescription, _upgradeName;
    public Sprite _upgradeIcon;
}
 