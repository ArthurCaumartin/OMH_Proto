using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Defense", menuName = "Defense Meta")]
public class DefenseMeta : ScriptableObject
{
    public Sprite _defenseIcon;
    public string _defenseName;
    [TextArea] public string _defenseDescription;
    public GameObject _defensePrefab;
    public int _defenseCost;
}
