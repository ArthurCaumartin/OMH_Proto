using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMapPart : MonoBehaviour
{
    [SerializeField] private Image _mapPartUI, _playerImage;

    [SerializeField] private string _partName;

    public string GetName()
    {
        return _partName;
    }

    public void RevealRoom()
    {
        _mapPartUI.color = Color.gray;
    }

    public void VisitRoom()
    {
        _mapPartUI.color = Color.white;
        _playerImage.enabled = true;
    }

    public void LeaveRoom()
    {
        _playerImage.enabled = false;
    }
}
