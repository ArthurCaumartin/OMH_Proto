using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAkEvPost : MonoBehaviour
{
    [SerializeField] public AK.Wwise.Event _EventToPost;

    public void PostEvent()
    {
        _EventToPost.Post(gameObject);
    }

}
