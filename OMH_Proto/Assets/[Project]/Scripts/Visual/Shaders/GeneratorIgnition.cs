using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneratorIgnition : MonoBehaviour
{
    [SerializeField] private InteractibleMetal MetalGenerator;
    public float off;
    public float on;
    [SerializeField] private Material _FonctionnementGenerateur;
    
    // public void Start()
    // {
    //     GeneratorIgnition._isGeneratorActivated.AddListener(ActivateGenerator);
    // }


    public void Update()
    {
        print("Le générateur est actif");
        float f = MetalGenerator._isGeneratorActivated ? on : off;
        _FonctionnementGenerateur.SetFloat("_BlinkingSpeed", f);
    }
}
