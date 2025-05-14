using UnityEngine;
using DG.Tweening;

public class PlayerDisolve : MonoBehaviour
{
    [SerializeField] private Material _anneArmorDamage;
    [SerializeField] private Material _anne;

    private void Start()
    {
        SetMatValue(1);
    }

    public void ShowPlayerWithDisolve(float duration)
    {
        DOTween.To((time) =>
        {
            SetMatValue(time);
        }, 0, 1, duration);
    }

    public void SetMatValue(float value)
    {
        _anne.SetFloat("_integrite", value);
        _anneArmorDamage.SetFloat("_integrite", value);
    }
}
