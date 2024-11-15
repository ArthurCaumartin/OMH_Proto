using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SpawnerInfos", fileName = "WaveSpawnTD", order = 4)]
public class SpawnerScriptable : ScriptableObject
{
    public List<Wave> _waves = new List<Wave>(); 
}

[Serializable]
public class Wave
{
    public int timeToSpawn;
    public int numberOfEnemies;
    public int durationOfSpawn;
    public bool hasBeenCalled;
}
