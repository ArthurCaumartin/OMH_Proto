using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeInStartScene : MonoBehaviour
{
    [SerializeField] private GameObject _musicObject;
    private Image _image;
    private float _timer;
    
    void Start()
    {
        _image = GetComponent<Image>();
        _image.color = new Color(0, 0, 0, 1);
        _image.DOFade(0, 1f);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= 1.5f) FinishFade(); 
    }

    void FinishFade()
    {
        if (_musicObject == null)
        {
            Destroy(gameObject);
            return;
        }
        _musicObject.SetActive(true);
        Destroy(gameObject);
    }
}
