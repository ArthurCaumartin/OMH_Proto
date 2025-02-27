using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomObjectEnabler : MonoBehaviour
{
    [Serializable]
    public struct GameObjectList
    {
        public string name;
        public List<GameObject> list;
    }

    [SerializeField] private List<GameObjectList> _objectList;

    private void Start()
    {
        EnableRandomObject();
    }

    private void EnableRandomObject()
    {
        foreach (GameObjectList objList in _objectList)
            foreach (GameObject go in objList.list)
                go.SetActive(false);

        for (int i = 0; i < _objectList.Count; i++)
        {
            int randomIndex = Random.Range(0, _objectList[i].list.Count);
            _objectList[i].list[randomIndex].SetActive(true);
        }
    }
}