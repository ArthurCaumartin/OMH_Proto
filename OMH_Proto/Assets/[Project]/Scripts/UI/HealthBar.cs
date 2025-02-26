using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _healthBarImage;

    public void SetFillAmount(float toSet, bool enableImage = true)
    {
        if (toSet > .99)
        {
            _canvas.gameObject.SetActive(false);
            return;
        }
        
        _canvas.gameObject.SetActive(enableImage);
        // _healthBarImage.fillAmount = toSet;
        print(toSet);
        RectTransform rt = _healthBarImage.transform as RectTransform;

        rt.offsetMin = new Vector2(-toSet, rt.offsetMin.y);
        rt.offsetMax = new Vector2(toSet, rt.offsetMax.y);
    }
}
