using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuCameraManager : MonoBehaviour
{
    [SerializeField] private SplineTransition _transition;
    [Space]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private int _splineMainIndex = 0;
    [Space]
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private int _splineOptionIndex = 1;
    [Space]
    [SerializeField] private GameObject _creditsMenu;
    [SerializeField] private int _splineCreditIndex = 2;

    public void MoveToCredits()
    {
        _mainMenu.SetActive(false);
        StartCoroutine(MoveUI(_creditsMenu));
        _transition.SetIndex(_splineCreditIndex);
    }

    public void MoveToMainMenu()
    {
        _optionsMenu.SetActive(false);
        _creditsMenu.SetActive(false);
        StartCoroutine(MoveUI(_mainMenu));
        _transition.SetIndex(_splineMainIndex);
    }

    public void MoveToOptions()
    {
        _mainMenu.SetActive(false);
        StartCoroutine(MoveUI(_optionsMenu));
        _transition.SetIndex(_splineOptionIndex);
    }

    private IEnumerator MoveUI(GameObject target)
    {
        yield return new WaitForSeconds(1.15f);
        target.SetActive(true);
    }
}
