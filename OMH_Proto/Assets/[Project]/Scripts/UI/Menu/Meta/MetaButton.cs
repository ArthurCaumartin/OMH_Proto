using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MetaButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buttonText;
    private Image _buttonSprite;
    private MetaManager _metaManager;
    private PrepGameMenu _prepGameMenu;
    public buttonInfos _infos;
    
    void Start()
    {
        _buttonSprite = GetComponent<Image>();
        _metaManager = GetComponentInParent<MetaManager>();
        _prepGameMenu = GetComponentInParent<PrepGameMenu>();
    }
    
    public void Initialize(buttonInfos buttonInfos)
    {
        _infos = buttonInfos;
        _buttonText.text = _infos._text;
        if(_infos._icon != null) _buttonSprite.sprite = _infos._icon;
    }
    
    public void ClickedMeta()
    {
        _metaManager.TryBuy(this);
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
