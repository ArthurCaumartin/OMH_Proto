using UnityEngine;

public class MobAnimationControler : MonoBehaviour
{
    [SerializeField] private string _velocityParameter;
    [SerializeField] private string _attackParameter;
    [SerializeField] private string _attackAnimationSpeedParameter;
    [Space]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PhysicsAgent _physicsAgent;
    private Animator _animator;
    private int _velocityHash;
    private int _attackHash;
    private int _attackAnimationSpeedHash;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        OnValidate();

        GetComponent<AnimationEvent>().Event.AddListener(OnAttackAnimationEnd);
    }

    private void OnValidate()
    {
        _attackHash = Animator.StringToHash(_attackParameter);
        _velocityHash = Animator.StringToHash(_velocityParameter);
        _attackAnimationSpeedHash = Animator.StringToHash(_attackAnimationSpeedParameter);
    }

    private void Update()
    {
        _animator.SetFloat(_velocityHash, _rigidbody.velocity.magnitude);
    }

    public void PlayAttackAnimation(float animationSpeed)
    {
        _physicsAgent.enabled = false;

        _animator.SetFloat(_attackAnimationSpeedHash, animationSpeed);
        _animator.SetTrigger(_attackHash);
    }

    public void OnAttackAnimationEnd()
    {
        _physicsAgent.enabled = true;
    }
}
