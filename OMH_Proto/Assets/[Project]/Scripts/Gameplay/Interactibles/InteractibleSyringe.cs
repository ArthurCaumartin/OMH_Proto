using System.Collections;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

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
    
    [SerializeField] private MeshRenderer _centriMaterial;
    private float _syringeShaderValue = 0;
    
    public override void Interact(PlayerInteract playerInteract, out bool cancelInteraction)
    {
        if (!_isDomeOpen)
        {
            cancelInteraction = true;
            _isDomeOpen = true;
            
            _rotatingObject.transform.DOLocalRotate(new Vector3(-30, 0, 0), 1);
            DOTween.To(() => _speed, x => _speed = x, 0f, 3f);
        }
        else
        {
            cancelInteraction = false;
            _syringeValue.Value += 1f;
            _getSyringe.Raise();
            
            _flasksObject.SetActive(false);
            DOTween.To(() => _syringeShaderValue, x => _syringeShaderValue = x, 1, 1f);
        
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            boxCollider.enabled = false;
        }
    }

    void Update()
    {
        _centriMaterial.material.SetFloat("_centrifugeuseeteinte", _syringeShaderValue);
        
        _flasksObject.transform.Rotate(_axis * (_direction * (Time.deltaTime * _speed)));
    }
}
