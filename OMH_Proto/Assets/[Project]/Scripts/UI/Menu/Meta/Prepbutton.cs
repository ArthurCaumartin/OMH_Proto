using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrepButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buttonText;
    private Image _buttonSprite;
    private PrepGameMenu _prepGameMenu;
    public buttonInfos _infos;
    
    void Start()
    {
        _buttonSprite = GetComponent<Image>();
        _prepGameMenu = GetComponentInParent<PrepGameMenu>();
    }
    
    public void Initialize(buttonInfos buttonInfos)
    {
        _infos = buttonInfos;
        _buttonText.text = _infos._text;
        if(_infos._icon != null) _buttonSprite.sprite = _infos._icon;
    }
    
    public void ClickedPrepGame()
    {
        if (_prepGameMenu.ClickSelectButton(_infos)) DeactivateButton();
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
}
