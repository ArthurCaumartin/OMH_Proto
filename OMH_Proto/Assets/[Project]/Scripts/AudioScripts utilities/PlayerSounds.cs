using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event _SoundShieldUp;
    [SerializeField] private AK.Wwise.Event _SoundShieldDown;
    
  public void OnShieldUp()
    {
        _SoundShieldUp.Post(gameObject);
    }
  public void OnShieldDown() 
    {
        _SoundShieldDown.Post(gameObject);
    }
}
