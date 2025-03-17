using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _littleMap, _tallMap;
    [SerializeField] private GameEvent _switchTallMapEvent, _switchLittleMapEvent;

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
}
