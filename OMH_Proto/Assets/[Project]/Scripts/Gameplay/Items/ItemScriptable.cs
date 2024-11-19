using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class ItemScriptable : ScriptableObject
{
    public string _itemName;
    public ItemStatsContainer _itemStats;
    public Sprite _itemSprite;
}


[Serializable]
public struct ItemStatsContainer
{
    public FloatReference turretHealth,
        turretDamages,
        turretAS,
        wallHealth,
        trapAS,
        trapDamages,
        trapEffectDuration,
        trapEffectStrenght;

    public void Multiply(ItemStatsContainer structToMultiply)
    {
        if (structToMultiply.turretHealth.Value != 0) turretHealth.Value *= structToMultiply.turretHealth.Value;
        if (structToMultiply.turretDamages.Value != 0) turretDamages.Value *= structToMultiply.turretDamages.Value;
        if (structToMultiply.turretAS.Value != 0) turretAS.Value *= structToMultiply.turretAS.Value;
        if (structToMultiply.wallHealth.Value != 0) wallHealth.Value *= structToMultiply.wallHealth.Value;
        if (structToMultiply.trapAS.Value != 0) trapAS.Value *= structToMultiply.trapAS.Value;
        if (structToMultiply.trapDamages.Value != 0) trapDamages.Value *= structToMultiply.trapDamages.Value;
        if (structToMultiply.trapEffectDuration.Value != 0) trapEffectDuration.Value *= structToMultiply.trapEffectDuration.Value;
        if (structToMultiply.trapEffectStrenght.Value != 0) trapEffectStrenght.Value *= structToMultiply.trapEffectStrenght.Value;
    }
}
