using UnityEngine;

public class ShieldBreaking : MonoBehaviour
{
    [SerializeField] private string _fadeValue = "_fadeOutValue";
    [SerializeField] private float _fadeSpeed = .2f;
    [SerializeField] private Shield _playerShield;
    [SerializeField] private Material _shieldMaterial;

    private void Start()
    {
        _playerShield._onShieldDown.AddListener(DestroyShield);
    }

    void Update()
    {
        if (_shieldMaterial.GetFloat(_fadeValue) == 0) return;
        _shieldMaterial.SetFloat(_fadeValue, Mathf.Clamp(_shieldMaterial.GetFloat(_fadeValue) - Time.deltaTime, 0, 1));
    }

    public void DestroyShield()
    {
        _shieldMaterial.SetFloat(_fadeValue, 1);
    }
}

