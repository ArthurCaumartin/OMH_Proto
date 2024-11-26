using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QTEUI : MonoBehaviour
{
    [SerializeField] private Image _imagePrefab;
    [SerializeField] private RectTransform _imageContainer;
    [Space]
    [SerializeField] private Sprite _upSprite;
    [SerializeField] private Sprite _downSprite;
    [SerializeField] private Sprite _rightSprite;
    [SerializeField] private Sprite _leftSprite;
    [SerializeField] private List<Image> _imageList;
    private Camera _mainCam;

    private void Start()
    {
        _mainCam = Camera.main;
    }

    public void ActivateUI(List<Vector2> inputList)
    {
        SetNewImageList(inputList);
    }

    private void SetNewImageList(List<Vector2> inputList)
    {
        ClearInputImage();
        // print($"InputList Lenth = {inputList.Count}");

        for (int i = 0; i < inputList.Count; i++)
        {
            // print("AIAI");
            Image newImage = Instantiate(_imagePrefab, _imageContainer);
            newImage.sprite = GetDirectionSprite(inputList[i]);
            _imageList.Add(newImage);
        }

        //? look at camera
        Vector3 newOrientation = (transform.position - _mainCam.transform.position).normalized;
        newOrientation.x = 0; //? for good alignement
        _imageContainer.transform.forward = newOrientation;
    }

    public void SetColor(int index, Color color)
    {
        _imageList[index].color = color;
    }

    public void ClearInputImage()
    {
        if (_imageList == null) return;
        for (int i = 0; i < _imageList.Count; i++)
            Destroy(_imageList[i].gameObject);
        _imageList.Clear();
    }

    public Sprite GetDirectionSprite(Vector2 direction)
    {
        if (direction == Vector2.up) return _upSprite;
        if (direction == Vector2.down) return _downSprite;
        if (direction == Vector2.right) return _rightSprite;
        if (direction == Vector2.left) return _leftSprite;
        return null;
    }
}
