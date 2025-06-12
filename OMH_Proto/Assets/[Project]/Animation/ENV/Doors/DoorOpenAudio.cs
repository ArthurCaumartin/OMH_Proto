using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenAudio : StateMachineBehaviour
{
    [SerializeField] private AK.Wwise.Event _doorOpenSound;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _doorOpenSound.Post(animator.gameObject);
    }
}
