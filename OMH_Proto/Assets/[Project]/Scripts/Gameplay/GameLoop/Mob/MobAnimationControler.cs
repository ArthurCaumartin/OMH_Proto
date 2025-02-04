using UnityEngine;

public class MobAnimationControler : MonoBehaviour
{
    [SerializeField] private AnimatorFloatSetter _velocityParametre;
    [SerializeField] private AnimatorTriggerSetter _attackParametre;
    [Space]
    [SerializeField] private Rigidbody _rigidbody;

    private void Update()
    {
        _velocityParametre.Value = _rigidbody.velocity.magnitude;
    }

    public void PlayAttackAnimation()
    {
        _attackParametre.SetParametre();
    }
}
