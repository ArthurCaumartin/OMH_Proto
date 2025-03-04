using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class QTECodeUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _codeText;
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
        SetNewImageList();
        _canvas.enabled = true;
    }

    private void SetNewImageList()
    {
        ClearInputImage();

        //? look at camera
        Vector3 newOrientation = (transform.position - _mainCam.transform.position).normalized;
        newOrientation.x = 0; //? for good alignement
        _canvas.transform.forward = newOrientation;
    }

    public void SetGoodInputFeedBack(int index)
    {
        
    }

    public void SetBadInputFeedBack(int index)
    {
        
    }

    public void ClearInputImage()
    {
        _codeText.text = "";

        _canvas.enabled = false;
    }
}
