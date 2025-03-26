using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DepthOfField = UnityEngine.Rendering.Universal.DepthOfField;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu, _gameUI, _noGameUI;
    
    [SerializeField] private InventoryPauseMenu _inventoryPauseMenu;
    [SerializeField] private ItemMenu _itemMenu;
    
    [SerializeField] private GameEvent _pauseMenuEvent, _resumeMenuEvent;
    
    private bool _isPaused;
    
    private void OnEscape()
    {
        if (_inventoryPauseMenu._isInventoryOpen)
        {
            _inventoryPauseMenu.CloseInventory();
            return;
        }
        
        if (!_isPaused) Pause();
        else
        {
            ResumeMenu();
        }
    }
    
    private void Pause()
    {
        _pauseMenu.SetActive(true);
        
        _gameUI.SetActive(false);
        _noGameUI.SetActive(false);
        
        Time.timeScale = 0;
        
        Volume volume = Camera.main.gameObject.GetComponent<Volume>();
        DepthOfField depthOfField;
        if (volume.profile.TryGet<DepthOfField>(out depthOfField))
        {
            depthOfField.focalLength.value = 300f;
        }

        _isPaused = true;
        _pauseMenuEvent.Raise();
    }
    
    public void ResumeMenu()
    {
        _pauseMenu.SetActive(false);

        if (!_inventoryPauseMenu._isInventoryOpen && !_itemMenu._isItemSelectionMenuOpen)
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
        
        _noGameUI.SetActive(true);
        
        _isPaused = false;
        _resumeMenuEvent.Raise();
    } 
}
