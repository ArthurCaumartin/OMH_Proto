using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DepthOfField = UnityEngine.Rendering.Universal.DepthOfField;

public class InventoryPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryMenu, _gameUI, _gainItemUI;
    
    [SerializeField] private GameEvent _pauseMenuEvent, _resumeMenuEvent;

    [SerializeField] private ItemMenu _itemMenu;

    public bool _isInventoryOpen;
    
    public void OnOpenInventory()
    {
        _inventoryMenu.SetActive(true);
        _gameUI.SetActive(false);
        _gainItemUI.SetActive(false);

        _isInventoryOpen = true;
        
        Time.timeScale = 0;
        
        Volume volume = Camera.main.gameObject.GetComponent<Volume>();
        DepthOfField depthOfField;
        if (volume.profile.TryGet<DepthOfField>(out depthOfField))
        {
            depthOfField.focalLength.value = 300f;
        }
        
        
        _pauseMenuEvent.Raise();
    }

    public void CloseInventory()
    {
        _inventoryMenu.SetActive(false);
        _gainItemUI.SetActive(true);
        
        _isInventoryOpen = false;
        
        if (!_itemMenu._isItemSelectionMenuOpen)
        {
            _gameUI.SetActive(true);
            Time.timeScale = 1;
            
            Volume volume = Camera.main.gameObject.GetComponent<Volume>();
            DepthOfField depthOfField;
            if (volume.profile.TryGet<DepthOfField>(out depthOfField))
            {
                depthOfField.focalLength.value = 34f;
            }
        }
        
        _resumeMenuEvent.Raise();
    }
}
