using AK.Wwise;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event _footstepEvent;
    [SerializeField] private AK.Wwise.Event _gearsEvent;

    [SerializeField] private AK.Wwise.Switch _surfaceSwitch;
    [SerializeField] private LayerMask surfaceLayerMask;

    private string currentSurface = "Default";
    public void StepSound()
    {
        DetectSurface();
        // _surfaceSwitch.SetValue(gameObject, currentSurface);
        _footstepEvent.Post(gameObject);
    }

    public void DetectSurface()
    {
       
    }

}
