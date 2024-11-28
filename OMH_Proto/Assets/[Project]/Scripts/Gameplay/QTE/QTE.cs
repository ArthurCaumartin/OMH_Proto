using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QTE : MonoBehaviour
{
    [SerializeField] private int _lenght = 5;
    [SerializeField] private UnityEvent<bool> _onInputEvent;
    [SerializeField] private UnityEvent _onQTEWin;
    [SerializeField] private UnityEvent _onQTEKill;
    public UnityEvent<bool> OnInput { get => _onInputEvent; }
    public UnityEvent OnWin { get => _onQTEWin; }
    public UnityEvent OnKill { get => _onQTEKill; }

    private List<Vector2> _directionSequence;
    private int _index = 0;
    private QTEUI _qteUi;
    private bool _isRuning = false;
    public bool IsRuning { get => _isRuning; }

    private void Start()
    {
        _qteUi = GetComponent<QTEUI>();
    }

    public void StartQTE()
    {
        _isRuning = true;
        _index = 0;
        _directionSequence = QTESequence.RandomSequence(_lenght);
        _qteUi.ActivateUI(_directionSequence);
    }

    private void ResetQTE()
    {
        _isRuning = false;
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
        // print($"Current Direction = {_directionSequence[_index]} / Input Direction {inputDirection}");
        if (_directionSequence[_index] == inputDirection)
        {
            _qteUi.SetGoodInputFeedBack(_index);
            _onInputEvent.Invoke(true);
            _index++;
        }
        else
        {
            _qteUi.SetBadInputFeedBack(_index);
            _onInputEvent.Invoke(false);
        }

        if (_index + 1 > _directionSequence.Count)
        {
            _onQTEWin.Invoke();
            ResetQTE();
        }
    }
}
