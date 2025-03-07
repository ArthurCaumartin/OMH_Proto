using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeInStartScene : MonoBehaviour
{
    [SerializeField] private GameObject _musicObject;
    private Image _image;
    void Start()
    {
        _image = GetComponent<Image>();
        
        _image.DOFade(0, 1f);
    }

    void FinishFade()
    {
        _musicObject.SetActive(true);
        Destroy(gameObject);
    }
}
