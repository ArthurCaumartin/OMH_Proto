using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaHealVisual : MonoBehaviour
{
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private Transform _planeFx;
    [SerializeField] private ParticleSystem _particle;


    public void Update()
    {
        Vector3 colliderScale = new Vector3(_collider.radius, 1, _collider.radius);

        _planeFx.transform.localScale = colliderScale;
        ParticleSystem.ShapeModule shape = _particle.shape;
        shape.radius = colliderScale.x;
    }

}
