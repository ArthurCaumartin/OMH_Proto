using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeChooseButton : MonoBehaviour
{
    private Image _buttonSprite;
    private PrepGameMenu _prepGameMenu;
    public buttonInfos _upgradeMeta;
    
    public void Initialize(buttonInfos meta, PrepGameMenu prepGameMenu)
    {
        _prepGameMenu = prepGameMenu;
        _upgradeMeta = meta;
        if(_upgradeMeta._icon != null) _buttonSprite.sprite = _upgradeMeta._icon;
    }

    public void Clicked()
    {
        _prepGameMenu.ClickCancelSelect(_upgradeMeta);
        
        Destroy(gameObject);
    }
}
