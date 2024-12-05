using System.Collections.Generic;
using UnityEngine;

public class Placable : MonoBehaviour
{
    public bool placeOnCorridorRail = false;
    public FloatReference cost;

    [SerializeField] private GameObject _prefabToPlace;
    private List<GameObject> _blockObject = new List<GameObject>();

    public GameObject PrefabToPlace { get => _prefabToPlace; }
    public bool CanBePlaced { get => _blockObject.Count == 0; }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Defenses" || other.gameObject.layer == 15)
            _blockObject.Add(other.gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Defenses" || other.gameObject.layer == 15)
            if (_blockObject.Contains(other.gameObject)) _blockObject.Remove(other.gameObject);
    }
}
