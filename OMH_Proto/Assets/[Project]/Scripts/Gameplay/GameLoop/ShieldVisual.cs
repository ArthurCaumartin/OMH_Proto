using UnityEngine;

public class ShieldVisual : MonoBehaviour
{
    [SerializeField] private string _opacityParameterName = "_opacityshield";
    [SerializeField] private string _wavePosParameterName = "_vagueposition";
    [SerializeField] private float _delayBetweenWave = 5;
    [SerializeField] private float _waveDuration;

    private float _delayTime = 0;
    private float _waveTime = 0;
    private Material _mat;
    private Shield _shield;


    private void Start()
    {
        _mat = GetComponentInChildren<Renderer>().material;
        _shield = GetComponentInParent<Shield>();

        SetMatValue(0);
    }

    private void Update()
    {
        _delayTime += Time.deltaTime;
        if (_delayTime >= _delayBetweenWave)
        {
            _waveTime += Time.deltaTime;
            SetMatValue(_waveTime / _waveDuration);
            if (_waveTime >= _waveDuration) Reset();
        }
    }

    private void SetMatValue(float time)
    {
        _mat.SetFloat(_opacityParameterName, 1 - Mathf.Sin(time * Mathf.PI));
        _mat.SetFloat(_wavePosParameterName, time);
    }

    private void Reset()
    {
        _waveTime = 0;
        _delayTime = 0;
    }
}