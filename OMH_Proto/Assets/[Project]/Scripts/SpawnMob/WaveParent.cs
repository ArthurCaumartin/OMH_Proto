using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WaveParent", fileName = "WaveSpawnTD", order = 4)]
public class WaveParent : ScriptableObject
{
    public List<SpawnerScriptable> _spawnersInfos = new List<SpawnerScriptable>();
}
