using UnityEngine;

public class AnimatorFlaotSetter : AnimatorParametreSetter
{
    [SerializeField] private FloatReference _value;
    public override void SetParametre()
    {
        _animator.SetFloat(_parametreHash, _value.Value);
    }

    private void Update()
    {
        SetParametre();
    }
}
