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

    [SerializeField] private AK.Wwise.RTPC _RTPC_GroundWetness;

    private string currentSurface = "Default";
    private float _wetnessValue;
    public void StepSound()
    {
        DetectSurface();
        // _surfaceSwitch.SetValue(gameObject, currentSurface);
        _footstepEvent.Post(gameObject);
    }

    private void Update()
    {
        UpdateGroundWetnessRTPC();
        _RTPC_GroundWetness.SetGlobalValue(_wetnessValue);
    }

    private void UpdateGroundWetnessRTPC()
    {
        int decalCount = DecalManager.Instance != null ? DecalManager.Instance.ActiveDecalsCount : 0;
        _wetnessValue = Mathf.Clamp(decalCount * 65f, 0f, 65f);
        AudioDebugLog.LogAudio(this.GetType().ToString(), gameObject.name, "Trig Update Wetness");

    }

    public void DetectSurface()
    {
       
    }

}