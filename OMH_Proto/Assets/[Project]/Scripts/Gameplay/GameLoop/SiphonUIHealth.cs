using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.Timeline.Actions;
using UnityEngine.UI;

public class SiphonUIHealth : MonoBehaviour
{
    [SerializeField] private List<Image> _healthObjects;

    private int _currentHealth = 20;
    private Vector3 _startPos;
    private void Start()
    {
        _currentHealth = _healthObjects.Count - 1;
        _startPos = transform.position;
    }

    public void LostHealth()
    {
        RemoveHealthPoint(_healthObjects[_currentHealth]);
        _currentHealth--;
    }

    private void RemoveHealthPoint(Image image)
    {
        image.DOColor(Color.white, .2f);
        transform.DOShakePosition(.4f, 10, 20, 180)
        .OnComplete(() =>
        {
            transform.position = _startPos;
            Vector3 healhStartPos = image.transform.localPosition;
            Color healthColor = image.color;
            DOTween.To((time) =>
            {
                image.transform.localPosition
                 = Vector3.Lerp(healhStartPos, healhStartPos - new Vector3(0, 60, 0), time);

                image.color
                = Color.Lerp(healthColor, new Color(healthColor.r, healthColor.g, healthColor.b, 0), time);
            }, 0, 1, .4f)
            .OnComplete(() =>
            {
                Destroy(image);
            });
        });
    }
}
