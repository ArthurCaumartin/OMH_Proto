using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.Events;

public class ShieldBreaking : MonoBehaviour
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
        // playerShield.ShieldDown += () => DestroyShield();
        // playerShield.ShieldUp += () => ShieldIsUp();

        // playerShield.ShieldDown.AddListener(DestroyShield);
        // playerShield.ShieldUp.AddListener(DestroyShield);
    }

    public void DestroyShield()
    {
        print("DestroyShield");
        _shieldMaterial.SetColor("_FlashingColor", BreakColor);
        _shieldMaterial.SetFloat("_BlinkingSpeed", BreakBlinkingSpeed);
        //FlashingColor.SetColor("_FlashingColor", 1);
    }

    public void ShieldIsUp()
    {
        print("ShieldUp");
        _shieldMaterial.SetColor("_FlashingColor", UpColor);
        _shieldMaterial.SetFloat("_BlinkingSpeed", UpBlinkingSpeed);
        //_FlashingColor = AlarmFlashingColor; 
    }
}

