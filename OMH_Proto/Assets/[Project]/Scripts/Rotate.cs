using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Vector3 _axis = Vector3.up;
    [SerializeField] private float _speed = 5;
    [SerializeField] private int _direction;

    void Start() { _direction = Random.value > .5f ? -1 : 1; }

    void Update()
    {
        transform.Rotate(_axis * _speed * Time.deltaTime * _direction);
    }
}
