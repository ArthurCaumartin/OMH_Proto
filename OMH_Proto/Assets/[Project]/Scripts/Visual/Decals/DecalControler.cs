using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalControler : MonoBehaviour
{
    [SerializeField] private AnimationCurve _fadeOnLife;
    [SerializeField] private float _lifeTime;
    private float _currentTime;
    private float _startFade = 1;

    private DecalProjector _projector;

    private void Start()
    {
        _projector = GetComponent<DecalProjector>();
        if(_startFade == 0) _startFade = 1;
        if (DecalManager.Instance != null)
            DecalManager.Instance.RegisterDecal();
    }

    public void SetLifeTime(float value, float startFade = 1)
    {
        _lifeTime = value;
        _startFade = startFade;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        _projector.fadeFactor = Mathf.Lerp(_startFade, 0, _fadeOnLife.Evaluate(Mathf.InverseLerp(0, _lifeTime, _currentTime)));
        if (_currentTime >= _lifeTime) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (DecalManager.Instance != null)
            DecalManager.Instance.UnregisterDecal();
    }
}
