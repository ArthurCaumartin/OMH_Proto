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
    public buttonInfos _infos;

    private bool _isBought;
    
    void Awake()
    {
        _buttonSprite = GetComponent<Image>();
        _metaManager = GetComponentInParent<MetaManager>();
    }
    
    public void Initialize(buttonInfos buttonInfos)
    {
        _infos = buttonInfos;
        // _buttonText.text = _infos._text;
        if(_infos._icon != null) _buttonSprite.sprite = _infos._icon;
    }
    
    public void ClickedMeta()
    {
        _metaManager.TryBuy(this);
    }
    
    public bool Confirmed()
    {
        if (_isBought)
        {
            return false;
        }
        
        GetComponent<Button>().interactable = false;
        _buttonSprite.color = new Color32(0, 255, 0, 255);
        _isBought = true;
        return true;
    }
}
