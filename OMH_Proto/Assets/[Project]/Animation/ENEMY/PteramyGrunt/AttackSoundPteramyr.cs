using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSoundPteramyr : StateMachineBehaviour
{
    [SerializeField] private AK.Wwise.Event _soundPteramyr;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _soundPteramyr.Post(animator.gameObject);
    }


}
