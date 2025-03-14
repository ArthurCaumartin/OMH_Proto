using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SpawnerInfos", fileName = "WaveSpawnTD", order = 4)]
public class SpawnerScriptable : ScriptableObject
{
    public int _roomIDToSpawn;
    public int timeToSpawn;
    public List<TypesOfEnemies> _enemiesType = new List<TypesOfEnemies>();
    public int durationOfSpawn;
    public bool hasBeenCalled;
    
    public AnimationCurve testCurve;
    private void OnValidate()
    {
        testCurve.ClearKeys();
        testCurve.AddKey(0, 0);
        
        float xfloat = (float) ((double) timeToSpawn / 100);

        int tempInt = 0;
        for (int i = 0; i < _enemiesType.Count; i++)
        {
            tempInt += _enemiesType[i]._mobNumber;
        }
        
        float yfloat = (float) ((double) tempInt / 100);
        
        testCurve.AddKey(xfloat, yfloat);
    }
}

[Serializable]
public class TypesOfEnemies
{
    public GameObject _mobPrefab;
    public int _mobNumber;
}
