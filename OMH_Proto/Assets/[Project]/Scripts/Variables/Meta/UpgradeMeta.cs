using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade Meta")]
public class UpgradeMeta : ScriptableObject
{
    public int _upgradeCost;

    [Header("UI Data :")]
    public string _upgradeName;
    [TextArea] public string _upgradeDescription;
    public Sprite _upgradeIcon;
    [Space]

    [Header("Level Data :")]
    public int currentLevel;
    [TextArea] public string levelScaleDescription;
    public List<FloatReference> levelValue;

    public float GetUpgradeValue()
    {
        if (currentLevel > levelValue.Count - 1)
            return levelValue[levelValue.Count - 1].Value;

        if (currentLevel < 0)
            return levelValue[0].Value;

        return levelValue[currentLevel].Value;
    }

    private void OnValidate()
    {
        Upgradable.SetUpgradeValue();
    }
}
