using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _healthBarImage;
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
    }

    public void SetFillAmount(float toSet)
    {
        RectTransform rt = _healthBarImage.transform as RectTransform;
        _ratioTarget = toSet;

        rt.offsetMin = new Vector2(-_ratioTarget, rt.offsetMin.y);
        rt.offsetMax = new Vector2(_ratioTarget, rt.offsetMax.y);
    }

    private void Enable(bool value)
    {
        _canvas.enabled = value;
    }
}
