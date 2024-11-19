using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private ItemList _commonList, _rareList, _epicList;

    [SerializeField] private FloatVariable turretHealth,
        turretDamages,
        turretAS,
        wallHealth,
        trapAS,
        trapDamages,
        trapEffectDuration,
        trapEffectStrenght;

    public void GainItem()
    {
        
    }
}