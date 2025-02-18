using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour
{
    [SerializeField] private GameObject _itemMenu;
    [SerializeField] private ItemManager _itemManager;
    [SerializeField] private Image _itemSprite;
    [SerializeField] private TextMeshProUGUI _itemName, _itemDescription;
    
    public void OpenMenu()
    {
        Time.timeScale = 0;
        _itemMenu.SetActive(true);
        
        ItemScriptable itemToShow = _itemManager._playerItemsList._items[_itemManager._playerItemsList._items.Count - 1];

        _itemSprite.sprite = itemToShow._itemSprite;
        _itemName.text = itemToShow._itemName;
        _itemDescription.text = itemToShow._itemDescription;
    }
    public void CloseMenu()
    {
        _itemMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
