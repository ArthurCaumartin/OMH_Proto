using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShieldBreaking : MonoBehaviour
{
    [SerializeField] private Shield playerShield;
    [SerializeField] private Material _shieldMaterial;

    public void Start()
    {
        playerShield._onShieldDown.AddListener(DestroyShield);
        playerShield._onShieldUp.AddListener(ShieldIsUp);

        // playerShield.ShieldDown += () => DestroyShield();
        // playerShield.ShieldUp += () => ShieldIsUp();

        // playerShield.ShieldDown.AddListener(DestroyShield);
        // playerShield.ShieldUp.AddListener(DestroyShield);
    }

    public void DestroyShield()
    {
        print("DestroyShield Shader");
        _shieldMaterial.SetFloat("isShieldDown", 1);
    }

    public void ShieldIsUp()
    {
        print("ShieldUp Shader");
        _shieldMaterial.SetFloat("isShieldDown", 0); 
    }
}

