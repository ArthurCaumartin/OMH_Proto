using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeMetaManager : MonoBehaviour
{
    [SerializeField] private FloatVariable _pcen;
    [SerializeField] private UpgradesList _upgradesList;
    [Space]
    [SerializeField] private GameObject _upgradeButtonsParent;
    [SerializeField] private GameObject _upgradeButtonPrefab;
    [SerializeField] private GameObject _confirmMenu, _notEnoughPopup;

    private UpgradeMetaButton _tempUpgrade;
    public bool _isUpgradesReset;
    void Start()
    {
        _upgradesList._upgradesUnlocked = new List<UpgradeMeta>();
        _isUpgradesReset = true;
        
        for (int i = 0; i < _upgradesList._upgrades.Count; i++)
        {
            GameObject instantiatedObject = Instantiate(_upgradeButtonPrefab, _upgradeButtonsParent.transform);
            instantiatedObject.GetComponent<UpgradeMetaButton>().Initialize(_upgradesList._upgrades[i]);
        }
    }

    public void ClickUpgradeButton(UpgradeMetaButton upgradeMetaButton)
    {
        if (upgradeMetaButton._upgradeMeta._upgradeCost <= _pcen.Value)
        {
            _tempUpgrade = upgradeMetaButton;
            _confirmMenu.SetActive(true);
        }
        else
        {
            StartCoroutine(CancelUpgradeButton());
        }
    }

    public void ConfirmUpgradeButton()
    {
        _tempUpgrade.Confirmed();
        _pcen.Value -= _tempUpgrade._upgradeMeta._upgradeCost;
        _upgradesList._upgradesUnlocked.Add(_tempUpgrade._upgradeMeta);
    }

    public IEnumerator CancelUpgradeButton()
    {
        _notEnoughPopup.SetActive(true);
        yield return new WaitForSeconds(1f);
        _notEnoughPopup.SetActive(false);
    }
}
