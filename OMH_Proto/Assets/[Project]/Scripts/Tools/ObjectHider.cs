using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectHider : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsToHide;
    [SerializeField] private KeyCode _input;

    void Update()
    {
        if (Input.GetKeyUp(_input))
            foreach (var item in _objectsToHide)
                item.SetActive(!item.activeSelf);
    }
}
