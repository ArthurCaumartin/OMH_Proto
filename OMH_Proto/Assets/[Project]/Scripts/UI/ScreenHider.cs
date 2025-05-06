using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class ScreenHider : MonoBehaviour
{
    public static ScreenHider instance;
    [SerializeField] private Image _imageHider;

    void Awake()
    {
        if (instance) Destroy(instance.gameObject);
        instance = this;
    }

    public void HideScreenForDuration(float hideDuration, float fadeDuration, Action toDoWhenHide = null, Action toDoAfter = null)
    {
        _imageHider.DOColor(Color.black, fadeDuration);
        StartCoroutine(SetColorAfterDelay(_imageHider, fadeDuration, hideDuration, new Color(0, 0, 0, 0), toDoWhenHide, toDoAfter));
    }

    private IEnumerator SetColorAfterDelay(Image image, float fadeDuration, float delay, Color color, Action toDoWhenHide = null, Action toDoAfter = null)
    {
        yield return new WaitForSeconds(delay / 2);
        toDoWhenHide?.Invoke();
        yield return new WaitForSeconds(delay / 2);
        image.DOColor(color, fadeDuration)
        .OnComplete(() => toDoAfter?.Invoke());
    }
}
