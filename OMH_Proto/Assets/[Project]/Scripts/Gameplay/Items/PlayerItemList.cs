using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerItemList", menuName = "PlayerItems")]
public class PlayerItemList : ScriptableObject
{
    public List<ItemScriptable> _items = new List<ItemScriptable>();
}
