using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _healthBarImage;

    public void SetFillAmount(float toSet, bool enableImage = true)
    {
        _canvas.gameObject.SetActive(enableImage);
        _healthBarImage.fillAmount = toSet;
    }
}
