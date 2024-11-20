using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Placable : MonoBehaviour
{
    public FloatReference cost;
    [SerializeField] private UnityEvent _onPlace;
    public UnityEvent OnPlaceEvent { get => _onPlace; }
    private List<Placable> _placableInTriggerRange = new List<Placable>();

    public bool CanBePlaced()
    {
        bool value = _placableInTriggerRange.Count == 0;
        // print(value ? "Can be placed" : "Cannot be placed");
        return value;
    }

    public void CallPlaceEvent()
    {
        // print("Placable Event Call");
        _onPlace.Invoke();
    }

    public void OnTriggerEnter(Collider other)
    {
        Placable p = other.GetComponent<Placable>();
        if (!p) return;
        if (!_placableInTriggerRange.Contains(p)) _placableInTriggerRange.Add(p);
    }

    public void OnTriggerExit(Collider other)
    {
        Placable p = other.GetComponent<Placable>();
        if (!p) return;
        if (_placableInTriggerRange.Contains(p)) _placableInTriggerRange.Remove(p);
    }
}
