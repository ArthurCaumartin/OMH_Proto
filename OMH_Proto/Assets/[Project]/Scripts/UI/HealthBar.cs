using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _healthBarImage;

    [Header("Color : ")]
    [SerializeField] private float _colorSwapSpeed = 5;
    [SerializeField] private List<Image> _imageToRecolor;
    [SerializeField] private Gradient _colorgradient;
    [SerializeField] private Color _poisonColor;
    private RectTransform _barRectTransform;
    private float _delayBeforeFade;
    private float _ratioTarget;

    private void Start()
    {
        _barRectTransform = _healthBarImage.transform as RectTransform;
        _ratioTarget = 1;
        Enable(false);
    }

    void Update()
    {
        if (_ratioTarget > .95f)
        {
            _delayBeforeFade += Time.deltaTime;
            if (_delayBeforeFade > 2)
                Enable(false);
            return;
        }

        Enable(true);
        _delayBeforeFade = 0;

        if (_canvas.enabled)
            SetColor();
    }

    public void SetFillAmount(float toSet)
    {
        _ratioTarget = toSet;

        if (_barRectTransform == null) return;
        _barRectTransform.offsetMin = new Vector2(-_ratioTarget, _barRectTransform.offsetMin.y);
        _barRectTransform.offsetMax = new Vector2(_ratioTarget, _barRectTransform.offsetMax.y);
    }

    private void SetColor()
    {
        foreach (var item in _imageToRecolor)
        {
            if (TryGetPoison())
            {
                item.color = Color.Lerp(item.color, _poisonColor, Time.deltaTime * _colorSwapSpeed);
                continue;
            }
            item.color = Color.Lerp(item.color, _colorgradient.Evaluate(_ratioTarget), Time.deltaTime * _colorSwapSpeed);
        }
    }

    private bool TryGetPoison()
    {
        return transform.parent.GetComponentInChildren<PoisonEffect>();
    }

    private void Enable(bool value)
    {
        _canvas.enabled = value;
    }
}
