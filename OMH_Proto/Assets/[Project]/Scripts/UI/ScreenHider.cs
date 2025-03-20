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

    public void HideScreenForDuration(float hideDuration, float fadeDuration, Action toDoAfter = null)
    {
        _imageHider.DOColor(Color.black, fadeDuration);
        StartCoroutine(SetColorAfterDelay(_imageHider, fadeDuration, hideDuration, new Color(0, 0, 0, 0), toDoAfter));
    }

    private IEnumerator SetColorAfterDelay(Image image, float fadeDuration, float delay, Color color, Action toDoAfter = null)
    {
        yield return new WaitForSeconds(delay / 2);
        toDoAfter?.Invoke();
        yield return new WaitForSeconds(delay / 2);
        image.DOColor(color, fadeDuration);
    }
}
