using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseAudio : StateMachineBehaviour
{
    [SerializeField] private AK.Wwise.Event _doorCloseSound;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _doorCloseSound.Post(animator.gameObject);
    }
}
