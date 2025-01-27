using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeChooseButton : MonoBehaviour
{
    private Image _buttonSprite;
    private PrepGameMenu _prepGameMenu;
    private buttonInfos _infos;
    void Awake()
    {
        _buttonSprite = GetComponent<Image>();
        _prepGameMenu = GetComponentInParent<PrepGameMenu>();
    }
    
    public void Initialize(buttonInfos meta, PrepGameMenu prepGameMenu)
    {
        _prepGameMenu = prepGameMenu;
        _infos = meta;
        if(_infos._icon != null) _buttonSprite.sprite = _infos._icon;
    }

    public void Clicked()
    {
        _prepGameMenu.ClickCancelSelect(_infos);
        
        Destroy(gameObject);
    }
}
