using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefenseMeta : MonoBehaviour
{
    [SerializeField] private Sprite _defenseSprite;
    [SerializeField] private TextMeshProUGUI _defenseName;
    [SerializeField, TextArea] private TextMeshProUGUI _defenseDescription;
    [SerializeField] private GameObject _defensePrefab;
}
