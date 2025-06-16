using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSounds : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event _TurretGetHit;
    [SerializeField] private AK.Wwise.Event _TurretPlace;
    [SerializeField] private AK.Wwise.Event _TurretDie;

    // Start is called before the first frame update
    public void OnHit()
    {
        _TurretGetHit.Post(gameObject);
    }
    public void OnPlace()
    {
        _TurretPlace.Post(gameObject);
    }
    public void OnDie()
    {
        _TurretDie.Post(gameObject);
    }
}
