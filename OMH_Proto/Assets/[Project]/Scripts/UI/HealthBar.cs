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
    [SerializeField] private Color _poinsonColor;

    float _delayBeforeFade;
    private float _ratioTarget;

    private void Start()
    {
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
        RectTransform rt = _healthBarImage.transform as RectTransform;
        _ratioTarget = toSet;

        rt.offsetMin = new Vector2(-_ratioTarget, rt.offsetMin.y);
        rt.offsetMax = new Vector2(_ratioTarget, rt.offsetMax.y);
    }

    private void SetColor()
    {
        foreach (var item in _imageToRecolor)
        {
            if (TryGetPoison())
            {
                item.color = Color.Lerp(item.color, _poinsonColor, Time.deltaTime * _colorSwapSpeed);
                continue;
            }
            item.color = _colorgradient.Evaluate(_ratioTarget);
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
