using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade Meta")]
public class UpgradeMeta : ScriptableObject
{
    public int _upgradeCost;
    public string _upgradeName;
    [TextArea] public string _upgradeDescription;
    public Sprite _upgradeIcon;
}
 