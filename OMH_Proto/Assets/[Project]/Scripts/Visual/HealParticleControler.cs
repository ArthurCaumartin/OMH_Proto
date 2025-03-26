using UnityEngine;

public class HealParticleControler : MonoBehaviour
{
    [SerializeField] private float _particleEnableDuration;
    private ParticleSystem _particle;
    private Health _health;
    private float _time;

    public void Start()
    {
        _health = GetComponentInParent<Health>();
        _particle = GetComponentInChildren<ParticleSystem>();
        _health.OnHealEvent.AddListener(() => _time = _particleEnableDuration);
    }

    public void Update()
    {
        _particle.gameObject.SetActive(_time > 0);
        _time -= Time.deltaTime;
    }
}