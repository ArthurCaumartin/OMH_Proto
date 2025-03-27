using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ApparitionAnne : MonoBehaviour
{
    [SerializeField] private RespawnSequence _respawnSequence;
    [SerializeField] private Material _anneArmorDamage;
    [SerializeField] private Material _anne;
    [SerializeField] private bool active;
    [SerializeField] private float _integriteAnne;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

        _anne.SetFloat("_integrite", _integriteAnne);
        print ("Anne apparaît");
        _anneArmorDamage.SetFloat("_integrite", _integriteAnne);
        print ("l'armure de Anne apparaît");
    }

    public void IntegriteAnne(float duration)
    {
        _integriteAnne = 0;

        DOTween.To(() => _integriteAnne, x => _integriteAnne = x, 1f, duration);
    }   
}
