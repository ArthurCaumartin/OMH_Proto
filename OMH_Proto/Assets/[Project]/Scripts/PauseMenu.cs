using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    
    public void OnPauseMenu()
    {
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);
        if(Time.timeScale == 1) Time.timeScale = 0;
        else if(Time.timeScale == 0) Time.timeScale = 1;
    }
}
