using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableAkEvent : MonoBehaviour
{
    [SerializeField] public AK.Wwise.Event _SoundToPost;
    public void OnEnable()
        {
        _SoundToPost.Post(gameObject);
        } 

}
