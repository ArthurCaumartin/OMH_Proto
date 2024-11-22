using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleMetal : Interactibles
{
    [SerializeField] private GameEvent _gainMetal;
    
    public override void Interact()
    {
        if (!_isPlayerInRange) return;
        
        FloatVariable metalValue = new FloatVariable();
        
        for (int i = 0; i < _infosManager._variables.Count; i++)
        {
            if (_infosManager._variables[i]._variableName == "Artifact")
            {
                metalValue = _infosManager._variables[i]._floatVariable;
                break;
            }
        }

        _isGeneratorActivated = true;

        // metalValue.Value += 10;
        // _gainMetal.Raise();
        // Destroy(gameObject);
    }
    
    
    [SerializeField] private FloatReference _timerToGetRessource;
    
    private float _timer;
    private bool _isGeneratorActivated;

    private void Update()
    {
        if (!_isGeneratorActivated) return;
        
        _timer += Time.deltaTime;
        if (_timer >= _timerToGetRessource.Value)
        {
            GainRessource();
        }
    }

    public void ActivateGenerator()
    {
        _isGeneratorActivated = true;
    }

    private void GainRessource()
    {
        _timer = 0;
        
        _gainMetal.Raise();
    }
}
