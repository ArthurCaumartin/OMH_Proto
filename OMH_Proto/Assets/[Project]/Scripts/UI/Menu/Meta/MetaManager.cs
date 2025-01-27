using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct buttonInfos
{
    public string _name;
    public string _text;
    public Sprite _icon;
    public int _cost;
    public int index;
}
public class MetaManager : MonoBehaviour
{
    [SerializeField] private FloatVariable _pcen, _technolith;
    [SerializeField] private UpgradesMetaList _upgradesList;
    [Space]
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private GameObject _upgradesParent, _weaponsParent, _defensesParent, _notEnoughPopup;
    [Space]
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private GameObject _confirmButton;

    private MetaButton _tempUpgrade;
    public bool _isUpgradesReset;
    void Start()
    {
        _upgradesList._upgradesUnlocked = new List<UpgradeMeta>();
        _upgradesList._weaponsUnlocked = new List<WeaponMeta>();
        _upgradesList._defensesUnlocked = new List<DefenseMeta>();
        _isUpgradesReset = true;
        
        buttonInfos infos = new buttonInfos();
        
        for (int i = 0; i < _upgradesList._upgrades.Count; i++)
        {
            GameObject instantiatedObject = Instantiate(_buttonPrefab, _upgradesParent.transform);
            
            infos._name = _upgradesList._upgrades[i]._upgradeName;
            infos._text = _upgradesList._upgrades[i]._upgradeDescription;
            infos._icon = _upgradesList._upgrades[i]._upgradeIcon;
            infos._cost = _upgradesList._upgrades[i]._upgradeCost;
            infos.index = 0;
            
            instantiatedObject.GetComponent<MetaButton>().Initialize(infos);
        }
        for (int i = 0; i < _upgradesList._weapons.Count; i++)
        {
            GameObject instantiatedObject = Instantiate(_buttonPrefab, _weaponsParent.transform); 
            
            infos._name = _upgradesList._weapons[i]._weaponName;
            infos._text = _upgradesList._weapons[i]._weaponDescription;
            infos._icon = _upgradesList._weapons[i]._weaponIcon;
            infos._cost = _upgradesList._weapons[i]._weaponCost;
            infos.index = 1;

            instantiatedObject.GetComponent<MetaButton>().Initialize(infos);
        }
        for (int i = 0; i < _upgradesList._defenses.Count; i++)
        {
            GameObject instantiatedObject = Instantiate(_buttonPrefab, _defensesParent.transform);
             
            infos._name = _upgradesList._defenses[i]._defenseName;
            infos._text = _upgradesList._defenses[i]._defenseDescription;
            infos._icon = _upgradesList._defenses[i]._defenseIcon;
            infos._cost = _upgradesList._defenses[i]._defenseCost;
            infos.index = 2;

            instantiatedObject.GetComponent<MetaButton>().Initialize(infos);
        }
    }

    public void TryBuy(MetaButton upgradeMetaButton)
    {
        if (upgradeMetaButton._infos._cost < 5)
        {
           if (upgradeMetaButton._infos._cost <= _technolith.Value)
            {
                _tempUpgrade = upgradeMetaButton;
                ChangeTextInfos(upgradeMetaButton._infos);
                _confirmButton.SetActive(true);
            }
            else
            {
                StartCoroutine(CancelUpgradeButton());
            } 
        }
        else
        {
            if (upgradeMetaButton._infos._cost <= _pcen.Value)
            {
                _tempUpgrade = upgradeMetaButton;
                ChangeTextInfos(upgradeMetaButton._infos);
                _confirmButton.SetActive(true);
            }
            else
            {
                StartCoroutine(CancelUpgradeButton());
            }
        }
        
    }

    private void ChangeTextInfos(buttonInfos infos)
    {
        if(infos._icon) _image.sprite = infos._icon;
        _nameText.text = infos._name;
        _descriptionText.text = infos._text;
        _costText.text = $"Cost : {infos._cost}";
    }

    public void ConfirmBuy()
    {
        if(!_tempUpgrade.Confirmed()) return;
        
        if (_tempUpgrade._infos.index == 0)
        {
            _pcen.Value -= _tempUpgrade._infos._cost;
            
            for (int i = 0; i < _upgradesList._upgrades.Count; i++)
            {
                if (_upgradesList._upgrades[i]._upgradeName == _tempUpgrade._infos._name)
                {
                    _upgradesList._upgradesUnlocked.Add(_upgradesList._upgrades[i]);
                }
            }
        }
        
        else if(_tempUpgrade._infos.index == 1)
        {
            _technolith.Value -= _tempUpgrade._infos._cost;
            
            for (int i = 0; i < _upgradesList._weapons.Count; i++)
            {
                if (_upgradesList._weapons[i]._weaponName == _tempUpgrade._infos._name)
                {
                    _upgradesList._weaponsUnlocked.Add(_upgradesList._weapons[i]);
                }
            }
        }
        
        else if(_tempUpgrade._infos.index == 2)
        {
            _technolith.Value -= _tempUpgrade._infos._cost;
            
            for (int i = 0; i < _upgradesList._defenses.Count; i++)
            {
                if (_upgradesList._defenses[i]._defenseName == _tempUpgrade._infos._name)
                {
                    _upgradesList._defensesUnlocked.Add(_upgradesList._defenses[i]);
                }
            }
        }
    }
    
    public IEnumerator CancelUpgradeButton()
    {
        _notEnoughPopup.SetActive(true);
        yield return new WaitForSeconds(1f);
        _notEnoughPopup.SetActive(false);
    }
}
