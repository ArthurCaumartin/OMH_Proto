using UnityEngine;

public class AnimatorBoolSetter : MonoBehaviour
{
    public Animator _animator;
    [SerializeField] private string _parametreName;
    public int _parametreHash;

    private void OnValidate()
    {
        _parametreHash = Animator.StringToHash(_parametreName);
    }

    public virtual void SetParametre(bool value)
    {
        _animator.SetBool(_parametreHash, value);
    }
}
