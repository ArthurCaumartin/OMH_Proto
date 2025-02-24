using System.Collections.Generic;
using UnityEngine;

public class Upgradable : MonoBehaviour
{
    public static List<Upgradable> upgradables = new List<Upgradable>();
    protected virtual void Awake() => upgradables.Add(this);
    private void OnApplicationQuit() => upgradables.Clear();

    public virtual void Start()
    {
        SetUpgradeValue();
    }

    public static void SetUpgradeValue()
    {
        if (upgradables.Count == 0) return;
        foreach (var item in upgradables)
        {
            // print("Call upgrade on : " + item.name);
            item.UpdateUpgrade();
        }
    }

    public virtual void UpdateUpgrade()
    {
        // print($"Update upgrade value : {name}");
    }
}