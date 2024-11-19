using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private ItemList _commonList, _rareList, _epicList;

    [SerializeField] private ItemStatsContainer _baseStatsContainer, _multplierStatsContainer;

    public List<ItemScriptable> _playerItemsList = new List<ItemScriptable>();

    public void GainItem()
    {
        int randomNumber = Random.Range(0, 99);
        ItemScriptable itemToGain;

        if (randomNumber >= 65)
        {
            randomNumber = Random.Range(0, _commonList._itemsList.Count);

            itemToGain = _commonList._itemsList[randomNumber];
        }
        else if (randomNumber >= 25)
        {
            randomNumber = Random.Range(0, _rareList._itemsList.Count);

            itemToGain = _rareList._itemsList[randomNumber];
        }
        else
        {
            randomNumber = Random.Range(0, _epicList._itemsList.Count);

            itemToGain = _epicList._itemsList[randomNumber];
        }

        _playerItemsList.Add(itemToGain);
        
    }

    public void MultiplyStats(ItemStatsContainer itemsStats)
    {
        
    }
}