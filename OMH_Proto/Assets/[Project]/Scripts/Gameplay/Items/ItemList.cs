using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemList", menuName = "ItemList")]
public class ItemList : ScriptableObject
{
    public Rarity _listRarity;
    public List<ItemScriptable> _itemsList = new List<ItemScriptable>();
}

public enum Rarity
{
    common, rare, epic
}


