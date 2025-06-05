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
        // int decalCount = DecalManager.Instance != null ? DecalManager.Instance.PlayerDecalsCount : 0;
        // _wetnessValue = Mathf.Clamp(decalCount * 80f, 0f, 80f);
        // AudioDebugLog.LogAudio(this.GetType().ToString(), gameObject.name, "Trig Update Wetness");
        if (DecalManager.Instance != null && DecalManager.Instance.LastPlayerDecal != null)
        {
            float opacity = DecalManager.Instance.LastPlayerDecal.CurrentOpacity;
            _wetnessValue = Mathf.Lerp(0f, 85f, opacity); 
        }
        else
        {
            _wetnessValue = 0f;
        }

        _RTPC_GroundWetness.SetGlobalValue(_wetnessValue);
    }

    public void DetectSurface()
    {
       
    }

}