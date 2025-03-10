using UnityEngine;

public class AeraHealer : Upgradable
{
    [SerializeField] private UpgradeMeta _healPerSecondUpgrade;
    [SerializeField] private float _healPerSecond = 10;
    private DefensesFinder _finder;

    private void Start()
    {
        _finder = GetComponentInParent<DefensesFinder>();
    }

    void Update()
    {
        for (int i = 0; i < _finder.DefenseList.Count; i++)
        {
            Health h = _finder.DefenseList[i].GetComponent<Health>();
            h?.Heal(gameObject, _healPerSecond * Time.deltaTime);
        }
    }

    public override void UpdateUpgrade()
    {
        base.UpdateUpgrade();
        if(!_healPerSecondUpgrade) return;
        _healPerSecond = _healPerSecondUpgrade.GetUpgradeValue();
    }
}
