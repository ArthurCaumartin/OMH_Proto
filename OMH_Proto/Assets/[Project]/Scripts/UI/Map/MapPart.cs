using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPart : MonoBehaviour
{
    [SerializeField] private List<MapPart> _neighborMapParts = new List<MapPart>();

    [SerializeField] private GameEvent _mapEvent;

    [SerializeField] private string partName;

    [SerializeField] private bool _isRevealed, _isVisited, _isPlayerInRoom;
    
    public void PlayerInRoom()
    {
        _isVisited = true;
        _isPlayerInRoom = true;
        
        for (int i = 0; i < _neighborMapParts.Count; i++)
        {
            _neighborMapParts[i].GetRevealed();
        }
        _mapEvent.Raise();
    }
    public void PlayerNotInRoom()
    {
        _isPlayerInRoom = false;
    }

    public void GetRevealed()
    {
        // if (_isRevealed) return;
        
        _isRevealed = true;
    }
    
    public void GetInfos(MapManager _mapManagerScript)
    {
        _mapManagerScript.ReceiveInfos(_isRevealed, _isVisited, _isPlayerInRoom, partName);
    }
}
