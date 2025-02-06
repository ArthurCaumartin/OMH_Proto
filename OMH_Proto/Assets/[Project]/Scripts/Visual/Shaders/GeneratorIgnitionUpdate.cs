using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorIgnitionUpdate : MonoBehaviour
{

[SerializeField] private InteractibleMetal MetalGenerator;
    public float off;
    public float on;
    public float EmissiveValue;
    public Color ColorShaderOn;
    public Color ColorShaderOff;
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
        _FonctionnementGenerateur.SetFloat("_VitesseVague", isOn ? on : off);
        _FonctionnementGenerateur.SetFloat("_IntensiteEmission", isOn ? EmissiveValue : 3 ); 
        _FonctionnementGenerateur.SetColor("_ObjectColor", isOn ? ColorShaderOn : ColorShaderOff);
    }
}
