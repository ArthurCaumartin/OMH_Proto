using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Video;

public class RadialMenu : MonoBehaviour
{
    [SerializeField] private RadialButton _buttonPrefab;
    [SerializeField] private Placer _placer;
    [SerializeField] private float _popAnimationDuration;
    [SerializeField] private int _menuLenght = 3;
    [SerializeField] private float _size = 5;
    [SerializeField] private Color _debugFirst = Color.red;
    [SerializeField] private Color _debugLast = Color.blue;
    private List<RadialButton> _buttonList = new List<RadialButton>();

    private void Start()
    {
        BakeMenu();
        Open(false, true);
    }

    public void ButtonClic(int index)
    {
        // print("Clic on button_" + index);
        _placer.Select(index);
        Open(false, true);
    }

    private void BakeMenu()
    {
        if (_buttonList.Count > 0)
        {
            foreach (var item in _buttonList)
            {
                if (!item) continue;
                Destroy(item.gameObject);
            }
            _buttonList.Clear();
        }

        for (int i = 0; i < _menuLenght; i++)
        {
            float time = Mathf.InverseLerp(0, _menuLenght, i);
            // print("Time : " + time);
            float radialTime = Mathf.Lerp(0, 360, time);
            radialTime *= Mathf.Deg2Rad;
            // print("Raial Time : " + radialTime);

            Vector3 newPos = new Vector3(-Mathf.Cos(radialTime), Mathf.Sin(radialTime), 0);
            // print("New Pos for i = " + i + " => " + newPos);

            RadialButton b = Instantiate(_buttonPrefab, transform);
            b.transform.localPosition = newPos * _size;
            b.GetComponent<Image>().color = Color.Lerp(_debugFirst, _debugLast, time);
            b.Initialize(i, this);
            _buttonList.Add(b);
        }
    }

    public void DisableButton(bool value)
    {
        for (int i = 0; i < _buttonList.Count; i++)
            _buttonList[i].gameObject.SetActive(value);
    }

    public void Open(bool value, bool skipAnim = false)
    {
        transform.DOScale(value ? Vector3.one : Vector3.zero, skipAnim ? 0 : _popAnimationDuration)
        .OnComplete(() => DisableButton(value));
    }

    private void OnOpenRadialMenu(InputValue value)
    {
        Open(value.Get<float>() > .5f, true);
    }
}
