using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioUIMenuSounds : MonoBehaviour
{
    [SerializeField] public AK.Wwise.Event _AssociatedUISound;

    public void OnUIClick()
    {
        _AssociatedUISound.Post(gameObject);
    }
}
