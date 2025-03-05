using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private ItemList _commonList, _rareList;

    [SerializeField] private ItemStatsContainer _refsStatsContainer, _baseStatsContainer;
    [SerializeField] private ItemStatsContainer _multplierStatsContainer;

    [SerializeField] private ObjectUIManager _objectUIManager;
    [SerializeField] private GameEvent _gainItem;

    [Space]
    [SerializeField] private ItemMenu _itemMenu;

    public PlayerItemList _playerItemsList;
    private List<ItemScriptable> itemsToSelect = new List<ItemScriptable>();

    private void Awake()
    {
        _baseStatsContainer.turretDamages.Value = _refsStatsContainer.turretDamages.Value;
        _baseStatsContainer.turretHealth.Value = _refsStatsContainer.turretHealth.Value;
        _baseStatsContainer.turretAS.Value = _refsStatsContainer.turretAS.Value;
        _baseStatsContainer.wallHealth.Value = _refsStatsContainer.wallHealth.Value;
        _baseStatsContainer.trapAS.Value = _refsStatsContainer.trapAS.Value;
        _baseStatsContainer.trapDamages.Value = _refsStatsContainer.trapDamages.Value;
        _baseStatsContainer.trapEffectDuration.Value = _refsStatsContainer.trapEffectDuration.Value;
        _baseStatsContainer.trapEffectStrenght.Value = _refsStatsContainer.trapEffectStrenght.Value;
        
        _playerItemsList._items.Clear();
    }

    public void OpenItemSelection()
    {
        //Select Random items and add to a list
        
        int tempRandomInt = Random.Range(0, _commonList._itemsList.Count);
        itemsToSelect.Add(_commonList._itemsList[tempRandomInt]);
        int tempSecondRandomInt = tempRandomInt;
        while (tempSecondRandomInt == tempRandomInt)
        {
            tempSecondRandomInt = Random.Range(0, _commonList._itemsList.Count);
        }
        itemsToSelect.Add(_commonList._itemsList[tempSecondRandomInt]);
        
        int tempThirdRandomInt = Random.Range(0, _rareList._itemsList.Count);
        itemsToSelect.Add(_rareList._itemsList[tempThirdRandomInt]);
        
        //Activate UI
        _itemMenu.OpenItemMenu(itemsToSelect, this);
    }

    public void SelectItem(int itemId)
    {
        //Take the "itemId" object in list and AddItem()
        _playerItemsList._items.Add(itemsToSelect[itemId]);
        
        AddItem(_multplierStatsContainer, itemsToSelect[itemId]);
        
        _objectUIManager.AddObjectUI(itemsToSelect[itemId]._itemName, itemsToSelect[itemId]._itemDescription, itemsToSelect[itemId]._itemSprite);
        
        ModifyStats();
        
        _gainItem.Raise();
    }

    private void ModifyStats()
    {
        _baseStatsContainer.turretDamages.Value = _refsStatsContainer.turretDamages.Value * _multplierStatsContainer.turretDamages.Value;
        _baseStatsContainer.turretHealth.Value = _refsStatsContainer.turretHealth.Value * _multplierStatsContainer.turretHealth.Value;
        _baseStatsContainer.turretAS.Value = _refsStatsContainer.turretAS.Value * _multplierStatsContainer.turretAS.Value;
        _baseStatsContainer.wallHealth.Value = _refsStatsContainer.wallHealth.Value * _multplierStatsContainer.wallHealth.Value;
        _baseStatsContainer.trapAS.Value = _refsStatsContainer.trapAS.Value * _multplierStatsContainer.trapAS.Value;
        _baseStatsContainer.trapDamages.Value = _refsStatsContainer.trapDamages.Value * _multplierStatsContainer.trapDamages.Value;
        _baseStatsContainer.trapEffectDuration.Value = _refsStatsContainer.trapEffectDuration.Value * _multplierStatsContainer.trapEffectDuration.Value;
        _baseStatsContainer.trapEffectStrenght.Value = _refsStatsContainer.trapEffectStrenght.Value * _multplierStatsContainer.trapEffectStrenght.Value;
    }
    private void AddItem(ItemStatsContainer structToModify, ItemScriptable itemToGain)
    {
        float tempFloat = (float) ((double) itemToGain._itemStats.turretHealth.Value / 100);
        if (itemToGain._itemStats.turretHealth.Value != 0) structToModify.turretHealth.Value += tempFloat;
        tempFloat = (float) ((double) itemToGain._itemStats.turretDamages.Value / 100);
        if (itemToGain._itemStats.turretDamages.Value != 0) structToModify.turretDamages.Value += tempFloat;
        tempFloat = (float) ((double) itemToGain._itemStats.turretAS.Value / 100);
        if (itemToGain._itemStats.turretAS.Value != 0) structToModify.turretAS.Value += tempFloat;
        tempFloat = (float) ((double) itemToGain._itemStats.wallHealth.Value / 100);
        if (itemToGain._itemStats.wallHealth.Value != 0) structToModify.wallHealth.Value += tempFloat;
        tempFloat = (float) ((double) itemToGain._itemStats.trapAS.Value / 100);
        if (itemToGain._itemStats.trapAS.Value != 0) structToModify.trapAS.Value += tempFloat;
        tempFloat = (float) ((double) itemToGain._itemStats.trapDamages.Value / 100);
        if (itemToGain._itemStats.trapDamages.Value != 0) structToModify.trapDamages.Value += tempFloat;
        tempFloat = (float) ((double) itemToGain._itemStats.trapEffectDuration.Value / 100);
        if (itemToGain._itemStats.trapEffectDuration.Value != 0) structToModify.trapEffectDuration.Value += tempFloat;
        tempFloat = (float) ((double) itemToGain._itemStats.trapEffectStrenght.Value / 100);
        if (itemToGain._itemStats.trapEffectStrenght.Value != 0) structToModify.trapEffectStrenght.Value += tempFloat;
    }
}