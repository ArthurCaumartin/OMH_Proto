using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeChooseButton : MonoBehaviour
{
    private Image _buttonSprite;
    private PrepGameMenu _prepGameMenu;
    public UpgradeMeta _upgradeMeta;
    
    public void Initialize(UpgradeMeta meta, PrepGameMenu prepGameMenu)
    {
        _prepGameMenu = prepGameMenu;
        _upgradeMeta = meta;
        if(_upgradeMeta._upgradeIcon != null) _buttonSprite.sprite = _upgradeMeta._upgradeIcon;
    }

    public void Clicked()
    {
        _prepGameMenu.ClickCancelSelect(_upgradeMeta);
        
        Destroy(gameObject);
    }
}
