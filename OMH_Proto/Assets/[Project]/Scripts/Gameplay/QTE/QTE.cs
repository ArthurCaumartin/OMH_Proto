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

    public UnityEvent StartQTEScript;
    public UnityEvent ResetQTEScript;
    

    private bool _isRuning = false;
    public bool IsRuning { get => _isRuning; }

    public void StartQTE()
    {
        _isRuning = true;
        //Call QteMain start
        StartQTEScript.Invoke();
    }

    private void ResetQTE()
    {
        _isRuning = false;
        //Call QteMain reset
        ResetQTEScript.Invoke();
    }

    public void KillQTE()
    {
        ResetQTE();
        _onQTEKill.Invoke();
    }
}
