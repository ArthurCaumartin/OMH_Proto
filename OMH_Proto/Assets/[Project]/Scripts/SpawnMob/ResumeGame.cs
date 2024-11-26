using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGame : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    
    public void OnCloseMenu()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1; 
    }
}
