using DG.Tweening;
using UnityEngine;

public class DisolveEffect : MonoBehaviour
{
    [SerializeField] private string _parameterName = "_Alpha";
    [SerializeField] private float _disolveDuration = 1;
    [SerializeField] private Health _health;

    private Renderer[] _rendererArray;


    private void Start()
    {
        _rendererArray = GetComponentsInChildren<Renderer>();

        _health?.SetDelayBeforDestroy(_disolveDuration);
        _health?.OnDeathEvent.AddListener(() => Disolve(true));

        Disolve(false);
    }


    public void Disolve(bool isHiding)
    {
        // print("ksedfrgbj");
        foreach (var item in _rendererArray)
            item.material.SetFloat(_parameterName, isHiding ? 1 : 0);

        DOTween.To((time) =>
        {
            foreach (var item in _rendererArray)
                item.material.SetFloat(_parameterName, time);

        }, isHiding ? 1 : 0, isHiding ? 0 : 1, _disolveDuration);
    }
}
