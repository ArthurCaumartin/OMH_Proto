using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event _footstepEvent;
    [SerializeField] private AK.Wwise.Event _gearsEvent;

    public void StepSound()
    {
        _footstepEvent.Post(gameObject);
    }

    public void SwitchChange()
    {
        
    }

}
