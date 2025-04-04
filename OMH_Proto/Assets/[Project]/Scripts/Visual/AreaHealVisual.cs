using Unity.VisualScripting;
using UnityEngine;

public class AreaHealVisual : MonoBehaviour
{
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private float _apparitionSpeed = 5;
    [SerializeField] private float _lockDuration = 2;
    private float _currentScaleValue;
    private float _targetValue;
    private float _lockOnVisible;


    private void Start()
    {
        _lockOnVisible = 1000;
        SetVisualVisibility(0);
    }

    private void Update()
    {
        _lockOnVisible += Time.deltaTime;
        if (_lockOnVisible > _lockDuration)
            _currentScaleValue = Mathf.Lerp(_currentScaleValue, _targetValue, Time.deltaTime * _apparitionSpeed);
        else
            _currentScaleValue = Mathf.Lerp(_currentScaleValue, 1, Time.deltaTime * _apparitionSpeed);


        UpdateVisual(_currentScaleValue);
    }

    private void UpdateVisual(float scale)
    {
        Vector3 colliderScale = Vector3.one * _collider.radius * scale;
        transform.localScale = colliderScale;
    }

    public void SetVisualVisibility(float targetValue)
    {
        _targetValue = targetValue;
        if (targetValue == 1) _lockOnVisible = 0;
    }
}
