using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu, _inGameUi, _inventoryUI;

    public void Pause(bool boolean)
    {
        print("Pause : " + boolean);
        _pauseMenu.SetActive(!boolean);
        Time.timeScale = boolean ? 1 : 0;
        Camera.main.gameObject.GetComponent<Volume>().weight = boolean ? 0 : 1;
        
        _inGameUi.SetActive(boolean);
        _inventoryUI.SetActive(boolean);
    }
    
    public void ResumeMenu()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Camera.main.gameObject.GetComponent<Volume>().weight = 0;
    } 
}
