using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DecalManager : MonoBehaviour
{
    public static DecalManager Instance { get; private set; }

    private List<DecalIdentifier> _activeDecals = new List<DecalIdentifier>();

    public int PlayerDecalsCount => _activeDecals.Count(d => d.Type == DecalType.Player);
    private DecalControler _lastPlayerDecal;
    public DecalControler LastPlayerDecal => _lastPlayerDecal;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void RegisterDecal(DecalIdentifier decal)
    {
        _activeDecals.Add(decal);
        if (decal.Type == DecalType.Player)
            _lastPlayerDecal = decal.GetComponent<DecalControler>();
    }

    public void UnregisterDecal(DecalIdentifier decal)
    {
        _activeDecals.Remove(decal);
    }
}