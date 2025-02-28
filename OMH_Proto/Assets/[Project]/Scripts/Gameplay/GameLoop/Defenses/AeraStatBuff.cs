using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AeraStatBuff : Upgradable
{
    [SerializeField] private UpgradeMeta _statBuffUpgrade;
    private DefensesFinder _finder;
    private float _buffValue;
    private Dictionary<TurretCannon, float> buffValueBackup = new Dictionary<TurretCannon, float>();

    private void Start()
    {
        _finder = GetComponent<DefensesFinder>();
        _finder.OnDefenseAdd.AddListener((defense) => BuffStat(defense.GetComponentInChildren<TurretCannon>()));
        _finder.OnDefenseRemove.AddListener((defense) => RemoveBuff(defense.GetComponentInChildren<TurretCannon>()));
    }

    public void BuffStat(TurretCannon cannon)
    {
        // print("Try buff : " + cannon?.name);
        if (!cannon) return;
        buffValueBackup.Add(cannon, _buffValue);

        cannon._attackSpeedMultiplier += _buffValue;
        cannon._damagesMultiplier += _buffValue;
    }

    public void RemoveBuff(TurretCannon cannon)
    {
        // print("Try Remove buff : " + cannon?.name);
        if (!cannon || !buffValueBackup.ContainsKey(cannon)) return;

        float toRemove;
        buffValueBackup.TryGetValue(cannon, out toRemove);

        cannon._attackSpeedMultiplier -= toRemove;
        cannon._damagesMultiplier -= toRemove;

        buffValueBackup.Remove(cannon);
    }

    public override void UpdateUpgrade()
    {
        base.UpdateUpgrade();
        if(!_statBuffUpgrade) return;

        _buffValue = _statBuffUpgrade.GetUpgradeValue();
    }
}