using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Upgradable : MonoBehaviour
{
    public static List<Upgradable> upgradables = new List<Upgradable>();
    protected virtual void Awake()
    {
        upgradables = FindObjectsOfType<Upgradable>().ToList();
        SetUpgradeValue();
    }
    private void OnApplicationQuit() => upgradables.Clear();

    //* This start need to be call ba child class to SetUpgrade on start of the scene
    // public void Start()
    // {
    //     print("Upgradable Start");
    //     SetUpgradeValue();
    // }

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