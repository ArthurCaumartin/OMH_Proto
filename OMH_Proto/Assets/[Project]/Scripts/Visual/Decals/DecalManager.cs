using UnityEngine;

public class DecalManager : MonoBehaviour
{
    public static DecalManager Instance { get; private set; }

    public int ActiveDecalsCount { get; private set; }

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterDecal() => ActiveDecalsCount++;
    public void UnregisterDecal() => ActiveDecalsCount--;
}