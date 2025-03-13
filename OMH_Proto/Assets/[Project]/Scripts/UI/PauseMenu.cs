using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu, _gameUI, _noGameUI;
    
    [SerializeField] private InventoryPauseMenu _inventoryPauseMenu;
    
    [SerializeField] private GameEvent _pauseMenuEvent, _resumeMenuEvent;
    
    private bool _isPaused, _isInventoryOpen;
    
    private void OnEscape()
    {
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
        Camera.main.gameObject.GetComponent<Volume>().weight = 1;

        _isPaused = true;
        _pauseMenuEvent.Raise();
    }
    
    public void ResumeMenu()
    {
        _pauseMenu.SetActive(false);

        if (!_inventoryPauseMenu._isInventoryOpen)
        {
            _gameUI.SetActive(true);
            Time.timeScale = 1;
            Camera.main.gameObject.GetComponent<Volume>().weight = 0;
        }
        
        _noGameUI.SetActive(true);
        
        _isPaused = false;
        _resumeMenuEvent.Raise();
    } 
}
