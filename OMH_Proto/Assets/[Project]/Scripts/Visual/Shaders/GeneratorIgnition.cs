using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneratorIgnition : MonoBehaviour
{
    [SerializeField] private InteractibleMetal MetalGenerator;
    public float off;
    public float on;
    public float EmissiveValue;
    public float ParticleSpeed;
    private Material _FonctionnementGenerateur;

    public void Start()
    {
        _FonctionnementGenerateur = GetComponent<MeshRenderer>().material;
    }


    public void Update()
    {
        SetMaterialValue(MetalGenerator._isGeneratorActivated);
    }

    public void SetMaterialValue(bool isOn)
    {
        _FonctionnementGenerateur.SetFloat("_BlinkingSpeed", isOn ? on : off);
        _FonctionnementGenerateur.SetFloat("_EmissivePower", isOn ? EmissiveValue : 0 ); 
        _FonctionnementGenerateur.SetFloat("_ParticleSpeed", isOn ? ParticleSpeed : 0);
    }
}
