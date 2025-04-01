using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EssenceAspiration : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private bool _active;
    [SerializeField] private Material _essence;
    public float _vitesseAspiration = 0;
    [SerializeField] private float _transitionDuration;

    void Start()
    {
        _essence.SetFloat("_aspiration", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawnManager._defenseAsStarted || _active)
        {
            DOTween.To(() => _vitesseAspiration, x => _vitesseAspiration = x, 1f, _transitionDuration).SetEase(Ease.Linear);
            _essence.SetFloat("_aspiration", _vitesseAspiration);
            // print(_vitesseAspiration);
        }
    }
}
