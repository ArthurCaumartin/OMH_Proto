using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;

public class ShieldVisual : MonoBehaviour
{
    [SerializeField] private string _opacityParameterName = "_opacityshield";
    [SerializeField] private string _wavePosParameterName = "_vagueposition";
    [SerializeField] private float _delayBetweenWave = 5;
    [SerializeField] private float _waveDuration;

    private float _delayTime = 0;
    private float _waveTime = 0;
    private Material _mat;
    private Renderer _shieldRenderer;
    private Shield _shield;


    private void Start()
    {
        _shieldRenderer = GetComponentInChildren<Renderer>();
        _mat = _shieldRenderer.material;
        _shield = GetComponentInParent<Shield>();
        _shield._onPlayerDeath.AddListener(OnRespawn);

        SetMatValue(0);

        _shield._onShieldDown.AddListener(OnDamageTaken);
        _shield._onShieldUp.AddListener(() =>
        {
            enabled = true;
            _delayTime = _delayBetweenWave;
            _waveTime = 0;
        });
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

    private void OnRespawn()
    {
        _shieldRenderer.transform.localScale = Vector3.one * 2;
        SetMatValue(0);
        enabled = true;
        _delayTime = _delayBetweenWave;
        _waveTime = 0;
    }

    private void OnDamageTaken()
    {
        SetMatValue(0);
        DOTween.To((time) =>
        {
            enabled = false;
            SetMatValue(time);
            _shieldRenderer.transform.localScale =
            Vector3.Lerp(Vector3.one * 2, Vector3.one * 4, time);
        }, 0, 1, .25f)
        .OnComplete(() =>
        {
            _shieldRenderer.transform.localScale = Vector3.one * 2;
            SetMatValue(0);
        });
    }

    private void Reset()
    {
        _waveTime = 0;
        _delayTime = 0;
    }
}