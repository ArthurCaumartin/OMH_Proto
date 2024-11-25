using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalControler : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private AnimationCurve _fadeOnLife;
    private float _currentTime;
    private DecalProjector _projector;

    private void Start()
    {
        _projector = GetComponent<DecalProjector>();
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        //! sorry future me
        _projector.fadeFactor = Mathf.Lerp(1, 0, _fadeOnLife.Evaluate(Mathf.InverseLerp(0, _lifeTime, _currentTime)));
        if(_currentTime >= _lifeTime) Destroy(gameObject);
    }
}
