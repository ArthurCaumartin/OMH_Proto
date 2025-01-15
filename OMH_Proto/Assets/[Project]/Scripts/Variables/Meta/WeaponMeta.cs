using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponMeta : MonoBehaviour
{
    [SerializeField] private Sprite _weaponSprite;
    [SerializeField] private TextMeshProUGUI _weaponName;
    [SerializeField, TextArea] private TextMeshProUGUI _weaponDescription;
    [SerializeField] private GameObject _weaponPrefab;
}
