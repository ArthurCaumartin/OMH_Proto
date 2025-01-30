using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SyringeFilling : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _transitionSpeed = 1;
    [SerializeField] private FloatReference syringecharges;
    [SerializeField] private Material _syringeTexture;

    public float EmissivePower;
    float transitionTime = 0, _transitionToZeroTime;
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        // SetMaterialValue(MetalGenerator._isGeneratorActivated);
        if (syringecharges.Value >= 1)
        {
            transitionTime += Time.deltaTime * _transitionSpeed;
            if (transitionTime > 1) return;
            SetOverlayValue(transitionTime);
            _transitionToZeroTime = 0;
        }

        else
        {
            _transitionToZeroTime += Time.deltaTime * _transitionSpeed;
            if (_transitionToZeroTime > 1) return;
            SetOverlayValue(1-_transitionToZeroTime);
            transitionTime = 0;
        }
    }

    public void SetOverlayValue(float value)
    {
        _syringeTexture.SetFloat("_SyringeState", value);
    }
}
