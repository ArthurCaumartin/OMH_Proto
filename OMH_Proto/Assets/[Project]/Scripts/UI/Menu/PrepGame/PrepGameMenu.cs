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
    [SerializeField] private GameObject _buttonPrefab, _buttonParent, _buttonChoosePrefab;
    [SerializeField] private List<GameObject> _pointHolder = new List<GameObject>();
    [Space]
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private TextMeshProUGUI _infoName;
    [SerializeField] private Image _infoImage;
    
    private List<GameObject> _instantiatedButtons = new List<GameObject>();
    private int _upgradeSelectedIndex = 0;
    
    public void Awake()
    {
        _gameChooseMeta._upgradesChooseGame = new List<UpgradeMeta>();
        if (_upgradeMetaManager._isUpgradesReset) return;
        _upgradesMetaList._upgradesUnlocked = new List<UpgradeMeta>();
    }
    
    public void OnEnable()
    {
        for (int i = 0; i < _upgradesMetaList._upgradesUnlocked.Count; i++)
        {
            GameObject instantiatedObject = Instantiate(_buttonPrefab, _buttonParent.transform);
            _instantiatedButtons.Add(instantiatedObject);
            // instantiatedObject.GetComponent<MetaButton>().Initialize(_upgradesMetaList._upgrades[i]);
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
    
    public bool ClickSelectButton(MetaButton metaButton)
    {
        return false;
        // if (_upgradeSelectedIndex > 2)
        // {
        //     return false;
        // }
        //
        // UpgradeMeta tempUpgradeMeta = metaButton._upgradeMeta;
        // GameObject instantiatedObject = Instantiate(_buttonChoosePrefab, _pointHolder[_upgradeSelectedIndex].transform);
        // instantiatedObject.GetComponent<UpgradeChooseButton>().Initialize(tempUpgradeMeta, this); 
        //
        // _gameChooseMeta._upgradesChooseGame.Add(tempUpgradeMeta);
        // _upgradeSelectedIndex ++;
        //
        // _infoImage.sprite = tempUpgradeMeta._upgradeIcon;
        // _infoName.text = tempUpgradeMeta._upgradeName;
        // _infoText.text = tempUpgradeMeta._upgradeDescription;
        // return true;
    }

    public void ClickCancelSelect(UpgradeMeta metaButton)
    {
        _gameChooseMeta._upgradesChooseGame.Remove(metaButton);
        _upgradeSelectedIndex --;

        for (int i = 0; i < _instantiatedButtons.Count; i++)
        {
            // if(_instantiatedButtons[i].GetComponent<MetaButton>()._upgradeMeta == metaButton)
            // {
            //     _instantiatedButtons[i].GetComponent<MetaButton>().ActivateButton();
            // }
        }
        
        _infoImage.sprite = null;
        _infoName.text = "";
        _infoText.text = "";
    }
}
