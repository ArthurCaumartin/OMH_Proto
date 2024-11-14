public class AnimatorTriggerSetter : AnimatorParametreSetter
{
    public override void SetParametre()
    {
        _animator.SetTrigger(_parametreHash);
    }
}
