using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class ItemScriptable : ScriptableObject
{
    public string _itemName;
    [TextArea (1, 5)] public string _itemDescription;
    public Sprite _itemSprite;
    public ItemStatsContainer _itemStats;
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

    
}
