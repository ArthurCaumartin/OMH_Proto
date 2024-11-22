using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string _sceneToLoadName;

    public void LoadScene()
    {
        SceneManager.LoadScene(_sceneToLoadName);
    }
}
