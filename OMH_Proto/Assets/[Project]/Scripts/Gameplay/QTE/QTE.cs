using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QTE : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> _onInputEvent;
    [SerializeField] private UnityEvent _onQTEWin;
    [SerializeField] private UnityEvent _onQTEKill;
    public UnityEvent<bool> OnInput { get => _onInputEvent; }
    public UnityEvent OnWin { get => _onQTEWin; }
    public UnityEvent OnKill { get => _onQTEKill; }

    [SerializeField] private List<Vector2> _directionSequence;
    private int _index = 0;
    private QTEUI _qteUi;

    private void Start()
    {
        _qteUi = GetComponent<QTEUI>();
    }

    public void StartQTE(List<Vector2> directionSequence)
    {
        _index = 0;
        _directionSequence = directionSequence;
        _qteUi.ActivateUI(_directionSequence);
    }

    private void ResetQTE()
    {
        _directionSequence.Clear();
        _qteUi.ClearInputImage();
    }

    public void KillQTE()
    {
        ResetQTE();
        _onQTEKill.Invoke();
    }

    public void PlayInput(Vector2 inputDirection)
    {
        print($"Current Direction = {_directionSequence[_index]} / Input Direction {inputDirection}");
        if (_directionSequence[_index] == inputDirection)
        {
            _qteUi.SetColor(_index, Color.green);
            _onInputEvent.Invoke(true);
            _index++;
        }
        else
        {
            _onInputEvent.Invoke(false);
        }

        if (_index + 1 > _directionSequence.Count)
        {
            _onQTEWin.Invoke();
            ResetQTE();
        }
    }
}
