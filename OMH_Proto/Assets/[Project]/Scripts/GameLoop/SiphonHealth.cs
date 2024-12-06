using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiphonHealth : MonoBehaviour
{
    [SerializeField] private GameEvent _destroyEvent;
    [SerializeField] private FloatReference _health;

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
        if (other.tag == "Mob")
        {
            LostHP();
            Destroy(other.gameObject);
        }
    }
}
