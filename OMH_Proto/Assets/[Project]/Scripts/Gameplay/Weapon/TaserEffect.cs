using UnityEngine;

public class TaserEffect : MonoBehaviour
{
    [SerializeField, Range(0, 100)] float _slowEffectSrenght = 100;
    private float _duration;
    private float _lifeTime;

    public void Initialize(float duration)
    {
        _duration = duration;
        GetComponent<PhysicsAgent>().SlowAgent(_slowEffectSrenght, _duration);
    }

    private void Update()
    {
        _lifeTime += Time.deltaTime;
        if (_lifeTime > _duration)
        {
            Destroy(this);
        }
    }
}
