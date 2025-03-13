using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InventoryPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryMenu, _gameUI, _gainItemUI;
    
    [SerializeField] private GameEvent _pauseMenuEvent, _resumeMenuEvent;

    public bool _isInventoryOpen;
    
    public void OnOpenInventory()
    {
        _inventoryMenu.SetActive(true);
        _gameUI.SetActive(false);
        _gainItemUI.SetActive(false);

        _isInventoryOpen = true;
        
        Time.timeScale = 0;
        Camera.main.gameObject.GetComponent<Volume>().weight = 1;
        
        _pauseMenuEvent.Raise();
    }

    public void CloseInventory()
    {
        _inventoryMenu.SetActive(false);
        _gameUI.SetActive(true);
        _gainItemUI.SetActive(true);
        
        _isInventoryOpen = false;
        
        Time.timeScale = 1;
        Camera.main.gameObject.GetComponent<Volume>().weight = 0;
        
        _resumeMenuEvent.Raise();
    }
}
