using UnityEngine;
using UnityEngine.UI;

public class RadialButton : MonoBehaviour
{
    public int _buttonIndex;
    private Button _button;
    private RadialMenu _radialMenu;

    public void Initialize(int index, RadialMenu radialMenu)
    {
        _buttonIndex = index;
        _radialMenu = radialMenu;
    }

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClic);
    }

    private void OnClic()
    {
        _radialMenu.ButtonClic(_buttonIndex);
    }
}
