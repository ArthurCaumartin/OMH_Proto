using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSoundClose : StateMachineBehaviour
{
    [SerializeField] private AK.Wwise.Event _lockSound;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _lockSound.Post(animator.gameObject);
    }
}
