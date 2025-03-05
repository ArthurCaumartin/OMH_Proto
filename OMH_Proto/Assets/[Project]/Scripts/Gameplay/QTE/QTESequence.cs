using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class QTESequence : Upgradable
{
    [SerializeField] private int _lenght = 5;

    private List<Vector2> _directionSequence;
    private int _index = 0;
    private QTE _qte;
    private QTESequenceUI _qteUi;
    private bool _isRunning;

    private void Start()
    {
        _qteUi = GetComponent<QTESequenceUI>();
    }

    public void StartQTE(QTE qteManager)
    {
        _isRunning = true;
        _qte = qteManager;
        _qteUi.ActivateUI(RandomSequence(_lenght));
    }

    public void ResetQTE()
    {
        _isRunning = false;
        _qteUi.ClearInputImage();
    }
    
    private void PlayInput(Vector2 inputDirection)
    {
        if(!_isRunning) return;
        
        // print($"Current Direction = {_directionSequence[_index]} / Input Direction {inputDirection}");
        if (_directionSequence[_index] == inputDirection)
        {
            _qteUi.SetGoodInputFeedBack(_index);
            _qte.OnInput.Invoke(true);
            _index++;
        }
        else
        {
            _qteUi.SetBadInputFeedBack(_index);
            _qte.OnInput.Invoke(false);
        }

        if (_index + 1 > _directionSequence.Count)
        {
            _qte.OnWin.Invoke();
            ResetQTE();
        }
    }
    
    public void OnQTEDirection(InputValue value)
    {
        // print("QTE Direction");
        //! send direction to _currentQTE
        Vector2 valueVector = value.Get<Vector2>();
        if (valueVector == Vector2.zero) return;
        PlayInput(valueVector);
    }
    
    public List<Vector2> RandomSequence(int size)
    {
        _directionSequence = new List<Vector2>();
        for (int i = 0; i < size; i++)
        {
            Vector2 newV = Random.insideUnitCircle;
            newV.x = (newV.x >= 0f) ? 1f : -1f;
            newV.y = (newV.y >= 0f) ? 1f : -1f;

            if (Random.value > .5f)
                newV.x = 0;
            else
                newV.y = 0;

            _directionSequence.Add(newV);
        }
        
        return _directionSequence;
    }
}
