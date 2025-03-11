using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class QTESequenceUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _directionImagePrefab;
    [SerializeField] private RectTransform _imageBackground;
    [SerializeField] private RectTransform _imageContainer;
    [Space]
    [SerializeField] private AnimationCurve _inputAlphaCurve;
    [SerializeField] private AnimationCurve _inputScaleCurve;
    [SerializeField] private float _animDuration = .5f;
    [Space]
    [SerializeField] private Sprite _directionSprite;
    // [SerializeField] private Sprite _upSprite;
    // [SerializeField] private Sprite _downSprite;
    // [SerializeField] private Sprite _rightSprite;
    // [SerializeField] private Sprite _leftSprite;
    [SerializeField] private List<Image> _imageList;
    private Camera _mainCam;

    private void Awake()
    {
        _mainCam = Camera.main;
        _canvas.enabled = false;
        
        _canvas.worldCamera = Camera.main.GetUniversalAdditionalCameraData().cameraStack[Camera.main.GetUniversalAdditionalCameraData().cameraStack.Count - 1];
    }

    public void ActivateUI(List<Vector2> inputList)
    {
        SetNewImageList(inputList);
        _canvas.enabled = true;
    }

    private void SetNewImageList(List<Vector2> inputList)
    {
        ClearInputImage();
        // print($"InputList Lenth = {inputList.Count}");

        for (int i = 0; i < inputList.Count; i++)
        {
            // print("AIAI");
            Image newImage = Instantiate(_directionImagePrefab, _imageContainer);
            // newImage.sprite = GetDirectionSprite(inputList[i]);
            newImage.sprite = _directionSprite;
            newImage.transform.up = inputList[i];
            _imageList.Add(newImage);
        }

        float inputImageSize = (_directionImagePrefab.transform as RectTransform).sizeDelta.x * 1.2f;
        print("Size x : " + inputImageSize);
        _imageBackground.sizeDelta = new Vector2(inputList.Count * inputImageSize, _imageBackground.sizeDelta.y);

        //? look at camera
        Vector3 newOrientation = (transform.position - _mainCam.transform.position).normalized;
        newOrientation.x = 0; //? for good alignement
        _canvas.transform.forward = newOrientation;
    }

    public void SetGoodInputFeedBack(int index)
    {
        // print("index : " + index + " / " + "Image count " + _imageList.Count);
        Color startColor = _imageList[index].color;
        DOTween.To((time) =>
        {
            _imageList[index].transform.localScale =
            Vector3.Lerp(Vector3.one, Vector3.zero, _inputScaleCurve.Evaluate(time));

            float curvetime = _inputAlphaCurve.Evaluate(Mathf.InverseLerp(0, _animDuration, time));
            _imageList[index].color =
            Color.Lerp(startColor, new Color(startColor.r, startColor.g, startColor.b), Mathf.Lerp(1, 0, curvetime));
        }, 0, 1, _animDuration);
    }

    public void SetBadInputFeedBack(int index)
    {
        // print("index : " + index + " / " + "Image count " + _imageList.Count);
        // Vector2 startpos = _imageList[index].transform.localPosition;
        // _imageList[index].transform.DOShakePosition(_animDuration, 1, 10, 90)
        // .OnComplete(() => _imageList[index].transform.localPosition = startpos);
    }

    public void ClearInputImage()
    {
        if (_imageList == null) return;
        for (int i = 0; i < _imageList.Count; i++)
            if (_imageList[i]) Destroy(_imageList[i].gameObject);
        _imageList.Clear();

        _canvas.enabled = false;
    }

    // public Sprite GetDirectionSprite(Vector2 direction)
    // {
    //     if (direction == Vector2.up) return _upSprite;
    //     if (direction == Vector2.down) return _downSprite;
    //     if (direction == Vector2.right) return _rightSprite;
    //     if (direction == Vector2.left) return _leftSprite;
    //     return null;
    // }
}
