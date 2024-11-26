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
    private float _timer;
    private bool _isGeneratorActivated;

    public override void OnQTEInput(bool isInputValide)
    {
        if(isInputValide) _metalCount.Value += _metalGainPerInput.Value;
    }

    public override void OnQTEWin()
    {
        _isGeneratorActivated = true;

        // metalValue.Value += 10;
        // _gainMetal.Raise();
        // Destroy(gameObject);
    }

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
