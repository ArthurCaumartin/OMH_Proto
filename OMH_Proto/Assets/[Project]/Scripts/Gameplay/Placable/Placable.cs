using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Placable : MonoBehaviour
{
    public FloatReference cost;
    [SerializeField] private GameObject _prefabToPlace;
    public GameObject PrefabToPlace { get => _prefabToPlace; }
    public bool CanBePlaced { get => _placableInTriggerRange.Count == 0; }

    private List<Placable> _placableInTriggerRange = new List<Placable>();

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
