using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    
    public void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Camera.main.gameObject.GetComponent<Volume>().weight = 1;
    }
    
    public void ResumeMenu()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Camera.main.gameObject.GetComponent<Volume>().weight = 0;
    } 
}
