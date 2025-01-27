using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Meta Progression", menuName = "Meta")]
public class GameChooseMeta : ScriptableObject
{
    public WeaponMeta _weaponChoose;
    
    public List<buttonInfos> _upgradesChooseGame = new List<buttonInfos>();
    
    // [SerializeField] private WeaponMeta _defenseChoose;
}
