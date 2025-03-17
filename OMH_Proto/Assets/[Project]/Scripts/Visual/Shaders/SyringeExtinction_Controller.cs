using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class SyringeExtinction_Controller : MonoBehaviour
{
    [SerializeField] public Rotate FioleRotation;
    [SerializeField] private Transform _domeCentrifugeuse;
    [SerializeField] private InteractibleSyringe Syringe;
    public float _syringestate;
    public float _animationDuration;
    public GameObject _centrifugeusefioles;
    public GameObject _centrifugeuseDome;
    private Material _centrifugeuse;

    void Start()
    {
        _centrifugeuse = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        SetMaterialValue(_centrifugeusefioles.activeSelf==true);
        BOUGERLEDOME(Syringe._isDomeOpen);
    }
    public void BOUGERLEDOME (bool isOn)
    {
        _domeCentrifugeuse.DOLocalRotate(new Vector3(-30, 0, 0), _animationDuration);
    }
    public void SetMaterialValue(bool isTrue)
    {
        _centrifugeuse.SetFloat("_centrifugeuseeteinte", isTrue ? _syringestate : 1 );
        //DOTween.To(() => _syringestate, x => _syringestate = x, 1, duration);
    }
}
