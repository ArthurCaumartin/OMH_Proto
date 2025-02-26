using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DefensesFinder : MonoBehaviour
{
    public bool DEBUG = false;
    [SerializeField] private float _raduis = 5;
    [SerializeField] private string _defensesTag = "Defenses";
    private List<GameObject> _defenseList = new List<GameObject>();
    private Placer _placer;
    public List<GameObject> DefenseList { get => _defenseList; }

    public UnityEvent<GameObject> OnDefenseAdd;
    public UnityEvent<GameObject> OnDefenseRemove;

    private void OnValidate()
    {
        SphereCollider s = GetComponent<SphereCollider>();
        if (s) s.radius = _raduis;
    }

    void OnTriggerEnter(Collider other)
    {
        // print("Trigger with : " + other.name);
        if (other.tag == _defensesTag)
            AddDefense(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == _defensesTag)
            RemoveDefense(other.gameObject);
    }

    private void AddDefense(GameObject objToAdd)
    {
        if (!_defenseList.Contains(objToAdd))
        {
            _defenseList.Add(objToAdd);
            OnDefenseAdd.Invoke(objToAdd);
        }
    }

    private void RemoveDefense(GameObject objToAdd)
    {
        if (_defenseList.Contains(objToAdd))
        {
            _defenseList.Remove(objToAdd);
            OnDefenseRemove.Invoke(objToAdd);
        }
    }

    private void OnDrawGizmo()
    {
        if (!DEBUG) return;
        Gizmos.color = new Color(0, 1, 1, .2f);
        Gizmos.DrawSphere(transform.position, _raduis);
    }
}


