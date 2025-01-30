using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponChoose : MonoBehaviour
{
    [SerializeField] private UpgradesMetaList _upgradesMetaList;
    [SerializeField] private GameChooseMeta _gameChooseMeta;
    [SerializeField] private MetaManager _upgradeMetaManager;
    [SerializeField] private GameObject _buttonsPrefab, _buttonsParent;

    private List<GameObject> _buttons = new List<GameObject>();

    private void Awake()
    {
        if (_buttonsParent == null) _buttonsParent = gameObject;
        if (_upgradeMetaManager._isReset) return;
        _upgradesMetaList._weaponsUnlocked = new List<WeaponMeta>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _upgradesMetaList._weapons.Count; i++)
        {
            bool tempBool = false;
            for (int j = 0; j < _upgradesMetaList._weaponsUnlocked.Count; j++)
            {
                if (_upgradesMetaList._weapons[i] == _upgradesMetaList._weaponsUnlocked[j])
                {
                    tempBool = true;
                    GameObject instantiatedButton = Instantiate(_buttonsPrefab, _buttonsParent.transform);
                    _buttons.Add(instantiatedButton);
                    
                    instantiatedButton.GetComponent<WeaponChooseButton>().InitializeUnlocked(_upgradesMetaList._weaponsUnlocked[j]);
                }
            }

            if (!tempBool)
            {
                GameObject instantiatedButton = Instantiate(_buttonsPrefab, _buttonsParent.transform);
                _buttons.Add(instantiatedButton);
                    
                instantiatedButton.GetComponent<WeaponChooseButton>().InitializeLocked(_upgradesMetaList._weapons[i]);
            }
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            Destroy(_buttons[i]);
        }
        _buttons.Clear();
    }
    
    public void ClickWeapon(WeaponMeta weaponMeta)
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].GetComponent<WeaponChooseButton>().Reset();
        }
        
        _gameChooseMeta._weaponChoose = weaponMeta;
    }
}
