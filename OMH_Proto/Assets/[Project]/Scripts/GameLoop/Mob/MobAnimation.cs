using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAnimation : MonoBehaviour
{
    [SerializeField] private string _attackParameterName = "Attack";
    [SerializeField] private Animator _animator;
    public Action resetAfterAnimation;
    public Action doDamage;
    
    private int _attackHash;

    private void OnValidate()
    {
        _attackHash = Animator.StringToHash(_attackParameterName);
    }

    public void Attack()
    {
        _animator.SetTrigger(_attackHash);
    }

    public void DoDamage()
    {
        doDamage.Invoke();
    }

    public void ResetAfterAnimation()
    {
        resetAfterAnimation.Invoke();
    }
}
