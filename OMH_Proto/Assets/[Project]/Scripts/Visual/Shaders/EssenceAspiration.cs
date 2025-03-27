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
     private float _vitesseAspiration = 0;
     [SerializeField] private float _transitionTime;

    void Start()
    {
        _essence.SetFloat("_aspiration", 0);
    }

    // Update is called once per frame
    void Update()
    {
       if(_spawnManager._defenseAsStarted || _active) 
       {
            DOTween.To(() => _vitesseAspiration, x => _vitesseAspiration = x, 1f, _transitionTime);
            _essence.SetFloat("_aspiration", _vitesseAspiration);
            print (_vitesseAspiration);
       }
    }
    
}
