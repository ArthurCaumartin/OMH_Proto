using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


[CreateAssetMenu(menuName = "SpawnerInfos", fileName = "WaveSpawnTD", order = 4)]
public class SpawnerScriptable : ScriptableObject
{
    public List<Wave> _waves = new List<Wave>();
    
    public AnimationCurve testCurve;
    private void OnValidate()
    {
        testCurve.ClearKeys();
        testCurve.AddKey(0, 0);
        
        for (int i = 0; i < _waves.Count; i++)
        {
            float xfloat = (float) ((double) _waves[i].timeToSpawn / 100);
            float yfloat = (float) ((double) _waves[i].numberOfEnemies / 100);
            
            testCurve.AddKey(xfloat, yfloat);
        }
    }
}

[Serializable]
public class Wave
{
    public int timeToSpawn;
    public int numberOfEnemies;
    public int durationOfSpawn;
    public bool hasBeenCalled;
}
