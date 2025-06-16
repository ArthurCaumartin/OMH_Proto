using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTPC_Setter : MonoBehaviour
{
    [SerializeField] private AK.Wwise.RTPC RTPC_SliderState;

    [SerializeField] public AK.Wwise.Event _PlayRundown;
    [SerializeField] public AK.Wwise.Event _WaitRundown;
    public void OnPauseSlider()
    {
        _WaitRundown.Post(gameObject);
    }
    public void OnResumeSlider()
    {
        _PlayRundown.Post(gameObject);
    }
}
