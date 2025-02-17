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
    }

    [SerializeField] private DecalProjector _decalePrefab;
    [SerializeField] private SpawnParameter _damageParametre;
    [SerializeField] private SpawnParameter _killParametre;
    [Space]
    [SerializeField] public List<Texture2D> _textureList;
    private MobLife _mobLife;

    private void Start()
    {
        _mobLife = GetComponent<MobLife>();
        _mobLife.OnDamageTakenEvent.AddListener((damageDealer, damageType) => SpawnDecals(_damageParametre, damageType));
        _mobLife.OnDeathEvent.AddListener((mobLife, damageType) => SpawnDecals(_killParametre, damageType));
    }

    private void SpawnDecals(SpawnParameter parameter, DamageType type)
    {
        Vector3 offset = Random.insideUnitSphere * parameter.spawnPosOffsetMax.Value;
        offset.y = 0.01f;
        DecalProjector d = Instantiate(_decalePrefab, transform.position + offset, Quaternion.Euler(90f, 0, Random.Range(0, 360)));
        d.GetComponent<DecalControler>().SetLifeTime(Random.Range(parameter.minLifeTime.Value, parameter.maxLifeTime.Value));
        d.transform.localScale = Vector3.one * Random.Range(parameter.minSize.Value, parameter.maxSize.Value);

        if (_textureList.Count != 0)
        {
            d.material = new Material(d.material);
            d.material.SetTexture("_Decal", _textureList[Random.Range(0, _textureList.Count)]);
            d.material.SetFloat("_Mix", type == DamageType.Poison ? 0 : 1);
        }
    }
}
