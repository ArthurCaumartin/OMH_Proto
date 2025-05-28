using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    [SerializeField] private GameObject _movementCanvasObject, _shootCanvasObject, _placementCanvasObject;

    private bool _isMovementPassed, _isShootPassed;

    void Start()
    {
        if (_movementCanvasObject == null)
        {
            Debug.LogWarning("Tuto manager has variables not assigned");
            Destroy(gameObject);
        }
        
        _movementCanvasObject.SetActive(true);
        _shootCanvasObject.SetActive(false);
        _placementCanvasObject.SetActive(false);
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
                _placementCanvasObject.SetActive(true);
                _isShootPassed = true;
            }
        }

        if (_isMovementPassed && _isShootPassed)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _placementCanvasObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
