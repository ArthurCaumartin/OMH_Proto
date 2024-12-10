using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class DecalesSpawner : MonoBehaviour
{
    [Serializable]
    public struct SpawnParameter
    {
        public FloatReference minSize;
        public FloatReference maxSize;
        [Space]
        public FloatReference minLifeTime;
        public FloatReference maxLifeTime;
        [Space]
        public FloatReference spawnPosOffsetMax;
        [SerializeField] public List<Material> materialList;
    }

    [SerializeField] private DecalProjector _decalePrefab;
    [SerializeField] private SpawnParameter _damageParametre;
    [SerializeField] private SpawnParameter _killParametre;
    private EnemyLife _mobLife;

    private void Start()
    {
        _mobLife = GetComponent<EnemyLife>();
        _mobLife.OnDamageEvent.AddListener(() => SpawnDecals(_damageParametre));
        _mobLife.OnDeathEvent.AddListener((mobLife) => SpawnDecals(_killParametre));
    }

    private void SpawnDecals(SpawnParameter parameter)
    {
        Vector3 offset = Random.insideUnitSphere * parameter.spawnPosOffsetMax.Value;
        offset.y = 0.01f;
        DecalProjector d = Instantiate(_decalePrefab, transform.position + offset, Quaternion.Euler(90f, 0, Random.Range(0, 360)));
        d.GetComponent<DecalControler>().SetLifeTime(Random.Range(parameter.minLifeTime.Value, parameter.maxLifeTime.Value));
        d.transform.localScale = Vector3.one * Random.Range(parameter.minSize.Value, parameter.maxSize.Value);

        if (parameter.materialList.Count != 0)
            d.material = parameter.materialList[Random.Range(0, parameter.materialList.Count)];
    }
}
