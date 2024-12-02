using UnityEngine;

public class AnimatorFloatSetter : AnimatorParametreSetter
{
    [SerializeField] private FloatReference _value;
    public float Value { set => _value.Value = value; }
    
    public override void SetParametre()
    {
        _animator.SetFloat(_parametreHash, _value.Value);
    }

    private void Update()
    {
        SetParametre();
    }
}
