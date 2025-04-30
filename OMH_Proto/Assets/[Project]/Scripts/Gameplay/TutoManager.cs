using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    [SerializeField] private GameObject _movementCanvasObject, _shootCanvasObject;

    private bool _isMovementPassed;

    void Start()
    {
        if (_movementCanvasObject == null)
        {
            Debug.LogWarning("Tuto manager has variables not assigned");
            Destroy(gameObject);
        }
        
        _movementCanvasObject.SetActive(true);
        _shootCanvasObject.SetActive(false);
    }

    private void Update()
    {
        if (!_isMovementPassed)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _movementCanvasObject.SetActive(false);
                _shootCanvasObject.SetActive(true);
                _isMovementPassed = true;
            }
        }

        if (_isMovementPassed)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _shootCanvasObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
