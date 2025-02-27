using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Volume _volume;
    
    public void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
        _volume.weight = 1;
    }
    
    public void ResumeMenu()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        _volume.weight = 0;
    } 
}
