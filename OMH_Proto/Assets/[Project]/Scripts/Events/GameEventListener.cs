using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent _event;
    [SerializeField] private UnityEvent<bool> _response;

    private void OnEnable() { _event.RegisterListener(this); }
    private void OnDisable() { _event.UnRegisterListener(this); }

    public void OnEventRaise(bool eventValue)
    {
        _response.Invoke(eventValue);
    }
}