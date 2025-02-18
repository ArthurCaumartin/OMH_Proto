using System.Collections.Generic;
using UnityEngine;

public class Placable : MonoBehaviour
{
    [SerializeField] public FloatReference cost;
    [SerializeField] private GameObject _prefabToPlace;
    [Space]
    [SerializeField] private string _colorParameterName = "_ObjectColor";
    [SerializeField] private float _transitionSpeed = 5;
    [SerializeField] private Color _canPlaceColor;
    [SerializeField] private Color _canNotPlaceColor;

    [HideInInspector] public bool placeOnCorridorRail = false;
    private List<GameObject> _blockObject = new List<GameObject>();
    private Renderer[] _rendererArray;

    public GameObject PrefabToPlace { get => _prefabToPlace; }
    public bool CanBePlaced { get => _blockObject.Count == 0; }

    private void Start()
    {
        _rendererArray = GetComponentsInChildren<Renderer>();
    }

    private void Update()
    {
        SetMeshsColor();
    }

    private void SetMeshsColor()
    {
        if (_rendererArray.Length == 0) return;
        Color renderColorTarget = _blockObject.Count == 0 ? _canPlaceColor : _canNotPlaceColor;
        foreach (var item in _rendererArray)
        {
            item.material.SetColor(_colorParameterName
            , Color.Lerp(item.material.GetColor(_colorParameterName), renderColorTarget, Time.deltaTime * _transitionSpeed));
        }
    }

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
