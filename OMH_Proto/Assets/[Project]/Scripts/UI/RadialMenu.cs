using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    [SerializeField] private Button _buttonPrefab;
    [SerializeField] private int _menuLenght = 3;
    [SerializeField] private Color _red = Color.red;
    [SerializeField] private Color _blue = Color.blue;
    [SerializeField] private float _size = 5;
    [SerializeField] private List<Button> _buttonList = new List<Button>();

    private void Start()
    {
        BakeMenu();
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
            print("Time : " + time);

            float radialTime = Mathf.Lerp(0, 360, time);
            radialTime *= Mathf.Deg2Rad;
            print("Raial Time : " + radialTime);

            Vector3 newPos = new Vector3(-Mathf.Cos(radialTime), Mathf.Sin(radialTime), 0);
            print("New Pos for i = " + i + " => " + newPos);

            Button b = Instantiate(_buttonPrefab, transform);
            b.transform.localPosition = newPos * _size;
            b.GetComponent<Image>().color = Color.Lerp(_red, _blue, time);
            _buttonList.Add(b);
        }
    }

    public void Open(bool value)
    {

    }

    private void OnOpenRadialMenu(InputValue value)
    {
        Open(value.Get<float>() > .5f);
    }
}
