using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent _event;
    [SerializeField] private UnityEvent _response;

    private void OnEnable() { _event.RegisterListener(this); }
    private void OnDisable() { _event.UnRegisterListener(this); }

    public void OnEventRaise()
    {
        _response.Invoke();
    }
}