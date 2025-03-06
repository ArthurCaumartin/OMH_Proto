using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _littleMap, _tallMap;
    [SerializeField] private GameEvent _switchTallMapEvent, _switchLittleMapEvent, _openInventory, _pauseMenu;

    private bool _isMapTall;
    
    public void OnOpenMap()
    {
        if (_isMapTall)
        {
            _tallMap.SetActive(false);
            _littleMap.SetActive(true);
            _isMapTall = false;
            _switchLittleMapEvent.Raise();
        }
        else
        {
            _tallMap.SetActive(true);
            _littleMap.SetActive(false);
            _isMapTall = true;
            _switchTallMapEvent.Raise();
        }
    }

    public void OnOpenInventory()
    {
        _openInventory.Raise();
    }

    public void OnPauseMenu()
    {
        //TODO Nique sa mere
        _pauseMenu.Raise(false);
    }
}
