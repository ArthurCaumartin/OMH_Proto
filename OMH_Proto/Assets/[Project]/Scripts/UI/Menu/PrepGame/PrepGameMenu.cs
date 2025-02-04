using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepGameMenu : MonoBehaviour
{
    [SerializeField] private UpgradeMetaManager _upgradeMetaManager;
    [SerializeField] private UpgradesList _upgradesList;
    [SerializeField] private GameObject _buttonPrefab, _buttonParent;
    [SerializeField] private List<GameObject> _pointHolder = new List<GameObject>();
    
    private List<GameObject> _instantiatedButtons = new List<GameObject>();
    private int _upgradeSelectedIndex = 0;
    
    public void Awake()
    {
        if (_upgradeMetaManager._isUpgradesReset) return;
        _upgradesList.upgradesUnlocked = new List<UpgradeMeta>();
    }
    
    public void OnEnable()
    {
        for (int i = 0; i < _upgradesList.upgradesUnlocked.Count; i++)
        {
            GameObject instantiatedObject = Instantiate(_buttonPrefab, _buttonParent.transform);
            _instantiatedButtons.Add(instantiatedObject);
            instantiatedObject.GetComponent<UpgradeButton>().Initialize(_upgradesList.upgrades[i]);
        }
    }

    public void OnDisable()
    {
        int tempCounter = _instantiatedButtons.Count;
        for (int i = 0; i < tempCounter; i++)
        {
            Destroy(_instantiatedButtons[0]);
            _instantiatedButtons.RemoveAt(0);
        }
    }
    
    public void ClickSelectButton(UpgradeButton upgradeMetaButton)
    {
        GameObject instantiatedObject = Instantiate(_buttonPrefab, _pointHolder[_upgradeSelectedIndex].transform);
        instantiatedObject.GetComponent<UpgradeButton>().Initialize(upgradeMetaButton._upgradeMeta);
        
        _upgradeSelectedIndex++;
    }
}
