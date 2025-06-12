using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PteramyrSounds : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event _pteramyrAttackSound;
    [SerializeField] private AK.Wwise.Event _pteramyrCallSound;
    [SerializeField] private AK.Wwise.Event _pteramyrVocalsSound;
    [SerializeField] private AK.Wwise.Event _pteramyrDeath;

    public void AttackSound()
    {
        _pteramyrAttackSound.Post(gameObject);
    }
    public void CallSound() 
    { 
        _pteramyrCallSound.Post(gameObject); 
    }
    public void VocalSounds() 
    { 
        _pteramyrVocalsSound.Post(gameObject);
    }
    public void DeathSound()
    {
        _pteramyrDeath.Post(gameObject);
    }
}
