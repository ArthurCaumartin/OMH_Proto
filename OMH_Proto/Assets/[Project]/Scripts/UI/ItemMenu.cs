using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class ItemMenu : MonoBehaviour
{
    [SerializeField] private GameObject _itemMenu;
    [SerializeField] private Image _itemSprite1, _itemsSprite2, _imageSprite3;
    [SerializeField] private TextMeshProUGUI _itemName1, _itemName2, _itemName3;
    [SerializeField] private TextMeshProUGUI _itemDescription1, _itemDescription2, _itemDescription3;
    
    private ItemManager _itemManager;

    public void OpenItemMenu(List<ItemScriptable> itemsList, ItemManager itemManager)
    {
        _itemMenu.SetActive(true);
        _itemManager = itemManager;
        Time.timeScale = 0;
        Camera.main.gameObject.GetComponent<Volume>().weight = 1;
        
        _itemSprite1.sprite = itemsList[0]._itemSprite;
        _itemName1.text = itemsList[0]._itemName;
        _itemDescription1.text = itemsList[0]._itemDescription;
        
        _itemsSprite2.sprite = itemsList[1]._itemSprite;
        _itemName2.text = itemsList[1]._itemName;
        _itemDescription2.text = itemsList[1]._itemDescription;
        
        _imageSprite3.sprite = itemsList[2]._itemSprite;
        _itemName3.text = itemsList[2]._itemName;
        _itemDescription3.text = itemsList[2]._itemDescription; 
    }

    public void SelectItem(int itemId)
    {
        _itemManager.SelectItem(itemId);
        CloseMenu();
    }
    
    public void CloseMenu()
    {
        _itemMenu.SetActive(false);
        Time.timeScale = 1;
        Camera.main.gameObject.GetComponent<Volume>().weight = 0;
    }
}
