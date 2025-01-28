using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GeneratorBodyEmissionb : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private InteractibleMetal MetalGenerator;

    public Texture2D BaseColorEteint;
    public Texture2D BaseColorAllume;
    public Texture2D EmissiveEteint;
    public Texture2D EmissiveAllume;
    public float EmissivePower;
    float transitionTime = 0;
    private Material _CorpsGenerateur;
    public void Start()
    {
        _CorpsGenerateur = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    public void Update()
    {
        SetMaterialValue(MetalGenerator._isGeneratorActivated);
    }

    public void SetMaterialValue(bool isOn)
    {
        _CorpsGenerateur.SetFloat("_EmissivePower", isOn ? EmissivePower : 0.3f);

        if (transitionTime >= 1) return;
        transitionTime += Time.deltaTime;
        _CorpsGenerateur.SetTexture("_BaseColor", BlendTex(BaseColorEteint, BaseColorAllume, transitionTime));
        _CorpsGenerateur.SetTexture("_EmissionMap", BlendTex(EmissiveEteint, EmissiveAllume, transitionTime));
    }

    public Texture2D BlendTex(Texture2D textIn, Texture2D textOut, float time)
    {
        Texture2D toReturn = textIn;
        for (int x = 0; x < toReturn.width; x++)
            for (int y = 0; y < toReturn.height; y++)
                toReturn.SetPixel(x, y, Color.Lerp(textIn.GetPixel(x, y), textOut.GetPixel(x, y), time));
        return toReturn;
    }
}
