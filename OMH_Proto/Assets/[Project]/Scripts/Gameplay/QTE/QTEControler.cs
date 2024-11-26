using UnityEngine;
using UnityEngine.InputSystem;

public class QTEControler : MonoBehaviour
{
    private QTE _currentQTE;
    private PlayerMovement _playerMovement;
    private PlayerAim _playerAim;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAim = GetComponent<PlayerAim>();
    }

    public void PlayQTE(QTE qteToPlay)
    {
        print("play qte");
        _currentQTE = qteToPlay;
        _currentQTE.StartQTE(QTESequence.RandomSequence(5));

        EnableControler(false);

        _currentQTE.OnWin.AddListener(OnQTEFinish);
        _currentQTE.OnKill.AddListener(OnQTEFinish);
    }

    private void OnQTEFinish()
    {
        EnableControler(true);

        _currentQTE?.OnWin.RemoveListener(OnQTEFinish);
        _currentQTE?.OnKill.RemoveListener(OnQTEFinish);

        _currentQTE = null;
    }

    public void EnableControler(bool value)
    {
        _playerMovement.enabled = value;
        _playerAim.enabled = value;
    }

    public void OnLeaveQTE(InputValue value)
    {
        if (_currentQTE && value.Get<float>() > .5f)
        {
            _currentQTE.KillQTE();
            _currentQTE = null;
        }
    }

    public void OnQTEDirection(InputValue value)
    {
        if (!_currentQTE) return;
        print("QTE Direction");
        //! send direction to _currentQTE
        Vector2 valueVector = value.Get<Vector2>();
        if (valueVector == Vector2.zero) return;
        _currentQTE.PlayInput(valueVector);
    }
}
