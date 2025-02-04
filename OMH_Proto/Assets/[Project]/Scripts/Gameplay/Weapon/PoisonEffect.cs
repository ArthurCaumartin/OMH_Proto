using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : MonoBehaviour, IEffectable
{
    [SerializeField, Range(1, 5)] int _damagesPerSeconds = 1;
    [SerializeField] private float _duration = 5;
    [Space]
    private float _lifeTime;
    private float _poisonEffectTime;
    private SpriteRenderer _spriteRenderer;
    private MobLife _mobLife;
    private float _range; //! use for debug only
    private Vector3 _effectHitPos;

    public void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _mobLife = transform.parent.GetComponent<MobLife>();
        DoDamages();
    }

    public void InitializeEffect(float effectRange, Vector3 pos)
    {
        _range = effectRange;
        _effectHitPos = pos;
    }

    private void Update()
    {
        _poisonEffectTime += Time.deltaTime;
        if (_poisonEffectTime > 1)
        {
            _poisonEffectTime = 0;
            DoDamages();
        }

        _lifeTime += Time.deltaTime;
        if (_lifeTime > _duration)
        {
            Destroy(gameObject);
        }
    }

    public void DoDamages()
    {
        _mobLife.TakeDamages(_damagesPerSeconds);
    }

    private void OnDrawGizmos()
    {
        Color c = Color.yellow;
        Gizmos.color = new Color(c.r, c.g, c.b, .1f);
        Gizmos.DrawSphere(_effectHitPos, _range);
    }
}
