using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade Meta")]
public class UpgradeMeta : ScriptableObject
{
    public string _upgradeName;
    public int _upgradeCost;
    [TextArea] public string _upgradeDescription;
    public Sprite _upgradeIcon;
    [Space]
    public int currentLevel;
    public List<FloatReference> levelValue;

    public float GetUpgradeValue()
    {
        if (currentLevel > levelValue.Count - 1)
            return levelValue[levelValue.Count - 1].Value;
        return levelValue[currentLevel].Value;
    }

    private void OnValidate()
    {
        Upgradable.SetUpgradeValue();
    }
}
