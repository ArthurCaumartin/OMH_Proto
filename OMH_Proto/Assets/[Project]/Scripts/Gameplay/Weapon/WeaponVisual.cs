using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponVisual : MonoBehaviour
{
    [SerializeField] private GameObject _visual;
    [SerializeField] private float _duration;
    [SerializeField] private Material _material;
    [SerializeField] private List<Texture> _textures;

    void Start()
    {
        _visual.SetActive(false);
    }

    public void PlayVisual(Vector3 scale)
    {
        _visual.transform.localScale = scale;
        _visual.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        _visual.SetActive(true);
        DOTween.To((time) =>
        {
            _material.SetTexture("_BaseMap", _textures[(int)Mathf.Lerp(0, _textures.Count - 1, time)]);
        }, 0, 1, _duration)
        .OnComplete(() => {_visual.SetActive(false);});
    }
}
