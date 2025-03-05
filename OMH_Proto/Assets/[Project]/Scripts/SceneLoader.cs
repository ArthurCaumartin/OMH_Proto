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
        _image.gameObject.SetActive(true);
        _image.DOFade(1f, 1f);
        
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(_sceneToLoadName);
    }
}
