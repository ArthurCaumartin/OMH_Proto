using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MobAnimationControler : MonoBehaviour
{
    [SerializeField] private string _velocityParameter;
    [SerializeField] private string _attackParameter;
    [SerializeField] private string _chargeAttackParameter;
    [SerializeField] private string _stunParameter;
    [SerializeField] private float _exitStunDuration = .2f;
    [SerializeField] private string _attackAnimationSpeedParameter;
    [SerializeField] private string _walkTransitionParametre;
    [Space]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PhysicsAgent _physicsAgent;
    private Animator _animator;
    private int _velocityHash;
    private int _attackHash;
    private int _stunHash;
    private int _chargeAttackHash;
    private int _walkTransitionHash;
    private int _attackAnimationSpeedHash;
    private bool _stunRuning = false;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        OnValidate();
    }

    private void OnValidate()
    {
        _attackHash = Animator.StringToHash(_attackParameter);
        _velocityHash = Animator.StringToHash(_velocityParameter);
        _velocityHash = Animator.StringToHash(_velocityParameter);
        _stunHash = Animator.StringToHash(_stunParameter);
        _walkTransitionHash = Animator.StringToHash(_walkTransitionParametre);
        _attackAnimationSpeedHash = Animator.StringToHash(_attackAnimationSpeedParameter);
        _chargeAttackHash = Animator.StringToHash(_chargeAttackParameter);
    }

    private void Update()
    {
        _animator.SetFloat(_velocityHash, _rigidbody.velocity.magnitude);
    }

    public void SetWalkTransition(float ratio)
    {
        _animator.SetFloat(_walkTransitionHash, ratio);
    }

    public void PlayAttackAnimation(float animationSpeed)
    {
        _animator.SetFloat(_attackAnimationSpeedHash, animationSpeed);
        _animator.SetTrigger(_attackHash);
    }

    public void PlayChargeAttack(out float duration)
    {
        _animator.Play(_chargeAttackHash);
        duration =_animator.GetCurrentAnimatorStateInfo(0).length;
    }

    public void PlayStunAnimationn(float duration)
    {
        print("Play Stun Animation");
        if (_stunRuning) return;
        _animator.SetBool(_stunHash, true);
        StartCoroutine(WaitForStopStunAnim(duration - _exitStunDuration));
    }

    private IEnumerator WaitForStopStunAnim(float duration)
    {
        print("Start Reset Coroutine");

        _stunRuning = true;
        yield return new WaitForSeconds(duration);
        _stunRuning = false;
        _animator.SetBool(_stunHash, false);
    }

    public bool IsAttackAnimation()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName("Mob_Pterattack");
    }

    public bool IsChargeAttack()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName("Charge_Attack");
    }
}
