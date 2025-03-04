using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionLoader : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;
    
    
    void Start()
    {
        SceneManager.LoadSceneAsync(_sceneToLoad);
    }
}
