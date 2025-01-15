using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon Meta")]
public class WeaponMeta : ScriptableObject
{
    public Sprite _weaponIcon;
    public string _weaponName;
    [TextArea] public string _weaponDescription; 
    public GameObject _weaponPrefab;
    public int _weaponCost;
}
