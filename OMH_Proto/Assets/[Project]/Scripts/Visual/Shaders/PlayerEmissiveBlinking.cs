using UnityEngine;

public class PlayerEmissiveBlinking : MonoBehaviour
{
    [SerializeField] private Shield playerShield;
    [SerializeField] private Material _shieldMaterial;

    public Color UpColor;
    public Color BreakColor;
    public float UpBlinkingSpeed;
    public float BreakBlinkingSpeed;

    public void Start()
    {
        playerShield._onShieldDown.AddListener(DestroyShield);
        playerShield._onShieldUp.AddListener(ShieldIsUp);
        playerShield._onPlayerDeath.AddListener(ShieldIsUp);
        ShieldIsUp();
    }

    public void DestroyShield()
    {
        // print("DestroyShield");
        _shieldMaterial.SetColor("_FlashingColor", BreakColor);
        _shieldMaterial.SetFloat("_BlinkingSpeed", BreakBlinkingSpeed);
    }

    public void ShieldIsUp()
    {
        // print("ShieldUp");
        _shieldMaterial.SetColor("_FlashingColor", UpColor);
        _shieldMaterial.SetFloat("_BlinkingSpeed", UpBlinkingSpeed);
    }
}

