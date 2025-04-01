using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string _sceneToLoadName;
    [SerializeField] private Image _image;

    public void LoadScene()
    {
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        ScreenHider.instance.HideScreenForDuration(1f, 1f);
        
        // _image.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_sceneToLoadName);
    }
}
