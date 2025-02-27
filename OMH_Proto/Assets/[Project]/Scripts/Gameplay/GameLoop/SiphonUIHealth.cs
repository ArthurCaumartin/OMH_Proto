using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiphonUIHealth : MonoBehaviour
{
    [SerializeField] private List<GameObject> _healthObjects;

    private int _currentHealth = 20;
    
    public void LostHealth()
    {
        _currentHealth--;
        Destroy(_healthObjects[_currentHealth]);
    }
}
