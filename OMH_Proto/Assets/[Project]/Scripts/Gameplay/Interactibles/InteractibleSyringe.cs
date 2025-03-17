using System.Collections;
using UnityEngine;
using DG.Tweening;

public class InteractibleSyringe : Interactible
{
    [Space]
    [SerializeField] private FloatVariable _syringeValue;
    [SerializeField] private GameEvent _getSyringe;

    [Space]
    
    [SerializeField] private GameObject _centriDome, _flasksObject, _rotatingObject;
    [SerializeField] private Vector3 _axis = Vector3.up;
    [SerializeField] private float _speed = 1000;
    [SerializeField] private int _direction = 1;
    public bool _isDomeOpen;

    public override void Interact(PlayerInteract playerInteract, out bool cancelInteraction)
    {
        if (!_isDomeOpen)
        {
            cancelInteraction = true;
            _isDomeOpen = true;
            
            //_centriDome.SetActive(false);
            
            DOTween.To(() => _speed, x => _speed = x, 0f, 3f);
        }
        else
        {
            cancelInteraction = false;
            _syringeValue.Value += 1f;
            _getSyringe.Raise();
            
            _flasksObject.SetActive(false);
        
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            boxCollider.enabled = false;
        }
    }

    void Update()
    {
        if (!_isDomeOpen) return;
        
        _rotatingObject.transform.Rotate(_axis * (_direction * (Time.deltaTime * _speed)));
        _flasksObject.transform.Rotate(_axis * (_direction * (Time.deltaTime * _speed)));
    }
}
