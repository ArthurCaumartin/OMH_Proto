using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PrepGameMenu : MonoBehaviour
{
    [SerializeField] private MetaManager _upgradeMetaManager;
    [SerializeField] private GameChooseMeta _gameChooseMeta;
    [SerializeField] private UpgradesMetaList _upgradesMetaList;
    [SerializeField] private GameObject _buttonPrefab, _buttonParent, _buttonChoosePrefab, _buttonChooseParent;
    [SerializeField] private List<GameObject> _pointHolder = new List<GameObject>();
    [Space]
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private TextMeshProUGUI _infoName;
    [SerializeField] private Image _infoImage;
    
    private List<GameObject> _instantiatedButtons = new List<GameObject>();
    private int _upgradeSelectedIndex = 0;
    
    public void Awake()
    {
        _gameChooseMeta._upgradesChooseGame = new List<buttonInfos>();
        if (_upgradeMetaManager._isUpgradesReset) return;
        _upgradesMetaList._upgradesUnlocked = new List<UpgradeMeta>();
    }
    
    public void OnEnable()
    {
        for (int i = 0; i < _upgradesMetaList._upgradesUnlocked.Count; i++)
        {
            GameObject instantiatedObject = Instantiate(_buttonPrefab, _buttonParent.transform);
            _instantiatedButtons.Add(instantiatedObject);
            instantiatedObject.GetComponent<PrepButton>().Initialize(
                _upgradesMetaList._upgrades[i]._upgradeIcon,
                _upgradesMetaList._upgrades[i].name,
                _upgradesMetaList._upgrades[i]._upgradeDescription,
                _upgradesMetaList._upgrades[i]._upgradeCost
                );
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
    
    public bool ClickSelectButton(buttonInfos buttonInfos)
    {
        if (_upgradeSelectedIndex >= _pointHolder.Count)
        {
            return false;
        }
        
        buttonInfos tempInfos = buttonInfos;
        GameObject instantiatedObject = Instantiate(_buttonChoosePrefab, _buttonChooseParent.transform);
        instantiatedObject.GetComponent<UpgradeChooseButton>().Initialize(tempInfos, this); 
        
        _gameChooseMeta._upgradesChooseGame.Add(tempInfos);
        _upgradeSelectedIndex ++;
        
        _infoImage.sprite = tempInfos._icon;
        _infoImage.color = Color.white;
        _infoName.text = tempInfos._name;
        _infoText.text = tempInfos._text;
        return true;
    }

    public void ClickCancelSelect(buttonInfos buttonInfos)
    {
        _gameChooseMeta._upgradesChooseGame.Remove(buttonInfos);
        _upgradeSelectedIndex --;

        for (int i = 0; i < _instantiatedButtons.Count; i++)
        {
            if(_instantiatedButtons[i].GetComponent<PrepButton>()._infos._name == buttonInfos._name)
            {
                _instantiatedButtons[i].GetComponent<PrepButton>().ActivateButton();
            }
        }
        
        _infoImage.sprite = null;
        _infoImage.color = new Color(0, 0, 0, 0);
        _infoName.text = "";
        _infoText.text = "";
    }
}
