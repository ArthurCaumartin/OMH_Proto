using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventUICredits : MonoBehaviour
{
    public AK.Wwise.Event _CreditsAmb;

    private void OnEnable()
    {
        _CreditsAmb.Post(gameObject);
    }
    private void OnDisable()
    {
        _CreditsAmb.Stop(gameObject);
    }
}
