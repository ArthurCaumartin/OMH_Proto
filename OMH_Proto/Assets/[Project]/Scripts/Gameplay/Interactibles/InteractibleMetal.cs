using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleMetal : Interactible
{
    [Space]
    [SerializeField] private FloatReference _metalGainPerInput;
    [SerializeField] private FloatVariable _metalCount;
    [SerializeField] private GameEvent _gainMetal;
    [SerializeField] private FloatReference _timerToGetRessource;
    
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _material;
    
    private float _timer;
    private bool _isGeneratorActivated;

    public override void Interact(out bool cancelIteraction)
    {
        cancelIteraction = _isGeneratorActivated;
    }

    public override void OnQTEInput(bool isInputValide)
    {
        if (isInputValide) _metalCount.Value += _metalGainPerInput.Value;
    }

    public override void OnQTEWin()
    {
        _isGeneratorActivated = true;
        
        _meshRenderer.material = _material;

        // metalValue.Value += 10;
        _gainMetal.Raise();
        // Destroy(gameObject);
    } 

    private void Update()
    {
        base.Update();
        
        if (!_isGeneratorActivated) return;

        _timer += Time.deltaTime;
        if (_timer >= _timerToGetRessource.Value)
        {
            _timer = 0;
            GainRessource();
        }
    }

    public void ActivateGenerator()
    {
        _isGeneratorActivated = true;
    }

    private void GainRessource()
    {
        //TODO faire un update dans l'update plutot qu'avec un game event
        _gainMetal.Raise();
    }
}
