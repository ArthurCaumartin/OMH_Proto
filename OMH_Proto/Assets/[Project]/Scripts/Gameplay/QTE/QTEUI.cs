using System.Collections.Generic;
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

    private List<Image> _imageList = new List<Image>();

    public void ActivateUI(List<Vector2> inputList, Vector3 canvasWorldPos)
    {
        transform.position = canvasWorldPos;
        SetNewImageList(inputList);
    }

    private void SetNewImageList(List<Vector2> inputList)
    {
        ClearInputImage();
        for (int i = 0; i < inputList.Count; i++)
        {
            Image newImage = Instantiate(_imagePrefab, _imageContainer);
            newImage.sprite = GetDirectionSprite(inputList[i]);
            _imageList.Add(newImage);
        }
    }

    private void ClearInputImage()
    {
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
