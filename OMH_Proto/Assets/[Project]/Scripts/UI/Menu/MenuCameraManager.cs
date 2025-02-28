using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuCameraManager : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private Transform _creditsTransform, _optionsTransform, _mainMenuTransform;
    [Space]
    [SerializeField] private GameObject _optionsMenu, _mainMenu, _creditsMenu;

    public void MoveToCredits()
    {
        _mainMenu.SetActive(false);
        _camera.transform.DOMove(_creditsTransform.position, 1f);
        StartCoroutine(MoveUI(_creditsMenu));
    }

    public void MoveToMainMenu()
    {
        _optionsMenu.SetActive(false);
        _creditsMenu.SetActive(false);
        _camera.transform.DOMove(_mainMenuTransform.position, 1f);
        StartCoroutine(MoveUI(_mainMenu));
    }

    public void MoveToOptions()
    {
        _mainMenu.SetActive(false);
        _camera.transform.DOMove(_optionsTransform.position, 1f);
        StartCoroutine(MoveUI(_optionsMenu));
    }

    private IEnumerator MoveUI(GameObject target)
    {
        yield return new WaitForSeconds(1.15f);
        target.SetActive(true); 
    }
}
