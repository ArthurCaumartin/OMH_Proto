using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Meta Progression", menuName = "Meta")]
public class GameChooseMeta : ScriptableObject
{
    public WeaponMeta _weaponChoose;
    
    public List<UpgradeMeta> _upgradesChooseGame = new List<UpgradeMeta>();
    
    // [SerializeField] private WeaponMeta _defenseChoose;
}
