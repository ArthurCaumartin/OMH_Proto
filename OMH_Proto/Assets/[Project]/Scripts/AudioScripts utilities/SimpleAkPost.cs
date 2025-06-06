using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAkPost : MonoBehaviour
{
    [SerializeField] public AK.Wwise.Event _SoundToPost;

    public void PostTheSound()
    {
        _SoundToPost.Post(gameObject);
    }
    
}
