using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InfosManager", menuName = "Infos")]
public class InfosManager : ScriptableObject
{
    [Header("Values")]
    public FloatVariable metal;
    public FloatVariable syringe;
    public float key = 2;
    public bool artifact = false;

    [Header("Objects")]
    public List<GameObject> items;

    [Header("Stats")]
    public float movementSpeed;
    public float gunAttackSpeed;
    public float gunDamages;

    private void OnEnable()
    {
        Debug.Log("Reset Values");
        // metal.Value = 0;
        // syringe.Value = 2;
        // key = 2;
        // artifact = false;
    } 
}
