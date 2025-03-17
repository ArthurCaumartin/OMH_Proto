using UnityEngine;

public class AeraHealer : Upgradable
{
    [SerializeField] private UpgradeMeta _healPerSecondUpgrade;
    [SerializeField] private float _healPerSecond = 10;
    private DefensesFinder _finder;
    AreaHealVisual _visual;


    private void Start()
    {
        _finder = GetComponentInParent<DefensesFinder>();
        _visual = GetComponent<AreaHealVisual>();
    }

    void Update()
    {
        if (_finder.DefenseList.Count == 0)
        {
            _visual.SetVisualVisibility(0);
            return;
        }

        bool isAllFullLife = true;
        for (int i = 0; i < _finder.DefenseList.Count; i++)
        {
            Health h = _finder.DefenseList[i].GetComponent<Health>();
            h?.Heal(gameObject, _healPerSecond * Time.deltaTime);

            if (!h.IsFullLife) isAllFullLife = false;
        }
        _visual.SetVisualVisibility(isAllFullLife ? 0 : 1);
    }

    public override void UpdateUpgrade()
    {
        base.UpdateUpgrade();
        if (!_healPerSecondUpgrade) return;
        _healPerSecond = _healPerSecondUpgrade.GetUpgradeValue();
    }
}
