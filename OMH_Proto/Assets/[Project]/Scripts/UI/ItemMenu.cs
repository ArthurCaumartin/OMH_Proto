using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using DepthOfField = UnityEngine.Rendering.Universal.DepthOfField;

public class ItemMenu : MonoBehaviour
{
    [SerializeField] private GameObject _itemMenu, _itemMenuB, _gameUI;
    
    [SerializeField] private Image _itemSprite1, _itemsSprite2, _imageSprite3;
    [SerializeField] private TextMeshProUGUI _itemName1, _itemName2, _itemName3;
    [SerializeField] private TextMeshProUGUI _itemDescription1, _itemDescription2, _itemDescription3;
    
    [SerializeField] private Image _itemSpriteB1, _itemsSpriteB2;
    [SerializeField] private TextMeshProUGUI _itemNameB1, _itemNameB2;
    [SerializeField] private TextMeshProUGUI _itemDescriptionB1, _itemDescriptionB2;
    
    private ItemManager _itemManager;
    public bool _isItemSelectionMenuOpen;

    public void OpenItemMenu(List<ItemScriptable> itemsList, ItemManager itemManager, bool _isBuildB)
    {
        _isItemSelectionMenuOpen = true;
        
        _itemManager = itemManager;
        
        if(_isBuildB) _itemMenuB.SetActive(true);
        else _itemMenu.SetActive(true);
        
        _gameUI.SetActive(false);
        
        Time.timeScale = 0;
        
        Volume volume = Camera.main.gameObject.GetComponent<Volume>();
        DepthOfField depthOfField;
        if (volume.profile.TryGet<DepthOfField>(out depthOfField))
        {
            depthOfField.focalLength.value = 300f;
        }

        if (_isBuildB)
        {
            _itemSpriteB1.sprite = itemsList[0]._itemSprite;
            _itemNameB1.text = itemsList[0]._itemName;
            _itemDescriptionB1.text = itemsList[0]._itemDescription;
            
            _itemsSpriteB2.sprite = itemsList[2]._itemSprite;
            _itemNameB2.text = itemsList[2]._itemName;
            _itemDescriptionB2.text = itemsList[2]._itemDescription;
        }

        else
        {
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
    }

    public void SelectItem(int itemId)
    {
        _itemManager.SelectItem(itemId);
        CloseMenu();
    }
    
    public void CloseMenu()
    {
        _isItemSelectionMenuOpen = false;
        
        _itemMenuB.SetActive(false);
        _itemMenu.SetActive(false);
        _gameUI.SetActive(true);
        
        Time.timeScale = 1;
        
        Volume volume = Camera.main.gameObject.GetComponent<Volume>();
        DepthOfField depthOfField;
        if (volume.profile.TryGet<DepthOfField>(out depthOfField))
        {
            depthOfField.focalLength.value = 34f;
        }
    }
}
