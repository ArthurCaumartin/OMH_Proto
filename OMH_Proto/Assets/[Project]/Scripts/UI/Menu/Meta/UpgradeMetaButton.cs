using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMetaButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buttonText;
    private int _upgradeCost;
    private Image _buttonSprite;
    private UpgradeMetaManager _upgradeMetaManager;
    private PrepGameMenu _prepGameMenu;
    public UpgradeMeta _upgradeMeta;
    
    void Start()
    {
        _buttonSprite = GetComponent<Image>();
        _upgradeMetaManager = GetComponentInParent<UpgradeMetaManager>();
        _prepGameMenu = GetComponentInParent<PrepGameMenu>();
    }
    
    public void Initialize(UpgradeMeta meta)
    {
        _upgradeMeta = meta;
        _upgradeCost = _upgradeMeta._upgradeCost;
        _buttonText.text = _upgradeMeta._upgradeDescription;
        if(_upgradeMeta._upgradeIcon != null) _buttonSprite.sprite = _upgradeMeta._upgradeIcon;
    }

    public void ClickedMeta()
    {
        _upgradeMetaManager.ClickUpgradeButton(this);
    }

    public void ClickedPrepGame()
    {
        if (_prepGameMenu.ClickSelectButton(this)) DeactivateButton();
    }

    public void DeactivateButton()
    {
        GetComponent<Button>().interactable = false;
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
    }

    public void ActivateButton()
    {
        GetComponent<Button>().interactable = true;
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }

    public void Confirmed()
    {
        GetComponent<Button>().interactable = false;
        _buttonSprite.color = new Color32(0, 255, 0, 255);
    }
}
