using UnityEngine;

public class AnimatorParametreSetter : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] private string _parametreName;
    [SerializeField] protected int _parametreHash;

    private void OnValidate()
    {
        _parametreHash = Animator.StringToHash(_parametreName);
    }

    public virtual void SetParametre() //TODO faut delete l'heritage useless wow la dingz
    {

    }
}
