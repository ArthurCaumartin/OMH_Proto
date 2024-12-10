using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    
    public void OnPauseMenu()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void OnResumeMenu()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    } 
}
