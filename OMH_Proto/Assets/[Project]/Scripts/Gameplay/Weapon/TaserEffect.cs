using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TaserEffect : MonoBehaviour, IEffectable
{
    [SerializeField, Range(0, 100)] float _slowEffectSrenght = 100;
    [SerializeField] private float _duration = 3;
    [Space]
    [SerializeField] private float _visualRefreshPerSecond = 3;
    private float _lifeTime;
    private float _visualRefreshTime;
    private SpriteRenderer _spriteRenderer;
    private MobAttack _attack;
    private float _range; //! use for debug only
    private Vector3 _effectHitPos;

    public void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        RefreshVisual();
        transform.parent.GetComponent<PhysicsAgent>().SlowAgent(_slowEffectSrenght / 100, _duration);
        _attack = transform.parent.GetComponent<MobAttack>();
        _attack.enabled = false;
    }

    public void InitializeEffect(float effectRange, Vector3 pos)
    {
        _range = effectRange;
        _effectHitPos = pos;
    }

    private void Update()
    {
        _visualRefreshTime += Time.deltaTime;
        if (_visualRefreshTime > 1 / _visualRefreshPerSecond)
        {
            _visualRefreshTime = 0;
            RefreshVisual();
        }

        _lifeTime += Time.deltaTime;
        if (_lifeTime > _duration)
        {
            _attack.enabled = true;
            Destroy(gameObject);
        }
    }

    public void RefreshVisual()
    {
        // _spriteRenderer.transform.forward = (transform.position - Camera.main.transform.position).normalized;
        _spriteRenderer.transform.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        _spriteRenderer.transform.localScale = Vector3.one * Random.Range(1, 1.5f);
    }

    private void OnDrawGizmos()
    {
        Color c = Color.yellow;
        Gizmos.color = new Color(c.r, c.g, c.b, .1f);
        Gizmos.DrawSphere(_effectHitPos, _range);
    }
}
