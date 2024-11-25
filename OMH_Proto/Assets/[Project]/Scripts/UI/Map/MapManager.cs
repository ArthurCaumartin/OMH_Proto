using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Transform _uiPartsParent, _partsParent;
    
    private List<UIMapPart> _uiMapPartsList = new List<UIMapPart>();

    private List<MapPart> _mapPartsList = new List<MapPart>();

    private int indexOfList;

    private void Start()
    {
        foreach (Transform child in _uiPartsParent)
        {
            _uiMapPartsList.Add(child.GetComponent<UIMapPart>());
        }
        foreach (Transform child in _partsParent)
        {
            _mapPartsList.Add(child.GetComponent<MapPart>());
        }
        
        OnMapActualize();
    }

    public void OnMapActualize()
    {
        for (int i = 0; i < _mapPartsList.Count; i++)
        {
            indexOfList = i;
            _mapPartsList[i].GetInfos(this);
        }
    }

    public void ReceiveInfos(bool isRevealed, bool isVisited, bool isPlayerInRoom, string partName)
    {
        string uiPartName = _uiMapPartsList[indexOfList].GetName();
        for (int i = 0; i < _uiMapPartsList.Count; i++)
        {
            if (uiPartName == partName)
            {
                
                if (isRevealed && !isVisited)
                {
                    _uiMapPartsList[indexOfList].RevealRoom();
                }
                else if(isVisited && isPlayerInRoom)
                {
                    _uiMapPartsList[indexOfList].VisitRoom();
                }
                else if(isVisited && !isPlayerInRoom)
                {
                    _uiMapPartsList[indexOfList].LeaveRoom();
                }
            }
        }
    }
}
