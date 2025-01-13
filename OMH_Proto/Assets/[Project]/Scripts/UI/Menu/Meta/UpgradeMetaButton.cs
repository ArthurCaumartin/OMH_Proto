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
    public UpgradeMeta _upgradeMeta;
    
    void Start()
    {
        _buttonSprite = GetComponent<Image>();
    }
    
    public void Initialize(UpgradeMeta meta, UpgradeMetaManager upgradeMetaManager)
    {
        _upgradeCost = meta._upgradeCost;
        _buttonText.text = meta._upgradeDescription;
        if(meta._upgradeIcon != null) _buttonSprite.sprite = meta._upgradeIcon;
        _upgradeMetaManager = upgradeMetaManager;
        _upgradeMeta = meta;
    }

    public void Clicked()
    {
        _upgradeMetaManager.ClickUpgradeButton(this);
    }

    public void Confirmed()
    {
        GetComponent<Button>().interactable = false;
        _buttonSprite.color = new Color32(0, 255, 0, 255);
    }
}
