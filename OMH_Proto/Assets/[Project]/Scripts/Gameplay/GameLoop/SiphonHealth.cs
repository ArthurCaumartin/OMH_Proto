using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiphonHealth : MonoBehaviour
{
    [SerializeField] private GameEvent _destroyEvent, _winScreenEvent;
    [SerializeField] private FloatReference _health;
    private bool _isVictory;
    private bool _canTakeDamage = false;
    
    public void LostHP()
    {
        _health.Value--;
        if (_health.Value <= 0)
        {
            _destroyEvent.Raise();
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(!_canTakeDamage) return;
        if (other.tag == "Mob")
        {
            LostHP();
            Destroy(other.gameObject);
        }
        else if (other.tag == "Player")
        {
            if (_isVictory)
            {
                _winScreenEvent.Raise();
            }
        }
    }

    public void SetCanTakeDamage(bool value)
    {
        _canTakeDamage = value;
    }

    public void WinGame()
    {
        _isVictory = true;
    }

}
