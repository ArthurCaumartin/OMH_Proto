using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    [SerializeField] private Button _buttonPrefab;
    [SerializeField] private int _menuLenth = 3;
    public Color _red = Color.red;
    public Color _blue = Color.blue;
    [SerializeField] private float _size = 5;
    [SerializeField] private List<Button> _buttonList = new List<Button>();

    private void Start()
    {
        BakeMenu();
    }

    public void Select()
    {

    }

    private void BakeMenu()
    {
        if(_buttonList.Count > 0)
        {
            foreach (var item in _buttonList)
            {
                if(!item) continue;
                Destroy(item.gameObject);
            }
            _buttonList.Clear();
        }

        for (int i = 0; i < _menuLenth; i++)
        {
            float time = Mathf.InverseLerp(0, _menuLenth - 1, i);
            // print("Time : " + time);

            float radialTime = Mathf.Lerp(-360, 360, time);
            // print("Raial Time : " + radialTime);

            Vector3 newPos = new Vector3(Mathf.Cos(radialTime), Mathf.Sin(radialTime), 0);
            Button b = Instantiate(_buttonPrefab, transform);
            b.transform.localPosition = newPos * _size;
            b.GetComponent<Image>().color = Color.Lerp(_red, _blue, time);
            _buttonList.Add(b);
        }
    }





}
