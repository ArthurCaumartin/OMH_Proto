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

    
}
