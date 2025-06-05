using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalControler : MonoBehaviour
{
    [SerializeField] private AnimationCurve _fadeOnLife;
    [SerializeField] private float _lifeTime;
    private float _currentTime;
    private float _startFade = 1;

    [SerializeField] private DecalType _decalType = DecalType.Mob;
    public DecalType Type => _decalType;
    private DecalProjector _projector;

    public float CurrentOpacity => _projector != null ? _projector.fadeFactor : 0f;

    private void Start()
    {
        _projector = GetComponent<DecalProjector>();
        if (_startFade == 0) _startFade = 1;

        if (DecalManager.Instance != null)
        {
            var identifier = GetComponent<DecalIdentifier>();
            if (identifier != null)
                DecalManager.Instance.RegisterDecal(identifier);
        }
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
    {
        var identifier = GetComponent<DecalIdentifier>();
        if (identifier != null)
            DecalManager.Instance.UnregisterDecal(identifier);
    }
    }
}
