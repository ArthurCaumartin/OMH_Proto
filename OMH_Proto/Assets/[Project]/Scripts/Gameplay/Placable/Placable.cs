using System.Collections.Generic;
using UnityEngine;

public class Placable : MonoBehaviour
{
    public FloatReference cost;
    private List<Placable> _placableInTriggerRange = new List<Placable>();

    public bool CanBePlaced()
    {
        bool value = _placableInTriggerRange.Count == 0;
        // print(value ? "Can be placed" : "Cannot be placed");
        return value;
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
