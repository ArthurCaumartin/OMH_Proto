using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;

public class QTECodeUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _codeText;
    [SerializeField] private Image _textContainerImage;
    [Space]
    [SerializeField] private AnimationCurve _inputAlphaCurve;
    [SerializeField] private AnimationCurve _inputScaleCurve;
    [SerializeField] private float _animDuration = .5f;
    
    private Camera _mainCam;

    private void Awake()
    {
        _mainCam = Camera.main;
        _canvas.enabled = false;
    }

    public void ActivateUI()
    {
        SetNewText();
        _canvas.enabled = true;
    }

    private void SetNewText()
    {
        _codeText.text = "";

        //? look at camera
        Vector3 newOrientation = (transform.position - _mainCam.transform.position).normalized;
        newOrientation.x = 0; //? for good alignement
        _canvas.transform.forward = newOrientation;
    }

    public void SetGoodInputFeedBack(int value)
    {
        _codeText.text += value.ToString();
    }

    public void SetBadInputFeedBack()
    {
        _codeText.text = "";
        StartCoroutine(WrongNumber());
    }

    public void ResetText()
    {
        _codeText.text = "";

        _canvas.enabled = false;
    }

    public void WinCode()
    {
        _textContainerImage.color = new Color(0f, 1f, 0f, 1f);
    }

    private IEnumerator WrongNumber()
    {
        _textContainerImage.color = new Color(1f, 0f, 0f, 1f);
        yield return new WaitForSeconds(0.7f);
        _textContainerImage.color = new Color(1f, 1f, 1f, 1f);
    }
}
