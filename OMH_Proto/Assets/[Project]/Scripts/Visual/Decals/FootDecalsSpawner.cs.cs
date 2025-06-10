using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class FootDecalsSpawner : MonoBehaviour
{
    [Serializable]
    public class Feet
    {
        public Transform pivot;
        public Texture2D texture2D;
        public float canSpawnDecal;
        public bool asStep;
        [HideInInspector] public float lastBloodMixe = 0;
    }


    [SerializeField] private float _rayLenth = 5;
    [SerializeField] private float _decalLifeTime = 5;
    [SerializeField] private float _decalSpawnDuration = 5;
    [SerializeField] private DecalProjector _feetDecalPrefab;
    [Space]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _decalsLayer;
    [SerializeField] private List<Feet> _feetList = new List<Feet>();

    public UnityEvent<RaycastHit> OnStepEvent;

    void Start()
    {
        // OnStepEvent.AddListener((hit) => { print("YAAAAAA"); });
    }

    private void Update()
    {

        foreach (var item in _feetList)
        {
            Cast(item);
            item.canSpawnDecal -= Time.deltaTime;
            item.canSpawnDecal = Mathf.Clamp(item.canSpawnDecal, 0, 1000);
        }
        // Cast(_leftFeet);
        // Cast(_rightFeet);

        // _rightFeet.canSpawnDecal -= Time.deltaTime;
        // _rightFeet.canSpawnDecal = Mathf.Clamp(_rightFeet.canSpawnDecal, 0, 1000);

        // _leftFeet.canSpawnDecal -= Time.deltaTime;
        // _leftFeet.canSpawnDecal = Mathf.Clamp(_leftFeet.canSpawnDecal, 0, 1000);
    }

    private void Cast(Feet feet)
    {
        Debug.DrawRay(feet.pivot.position, feet.pivot.up * _rayLenth, Color.red);

        Physics.Raycast(feet.pivot.position, feet.pivot.up, out RaycastHit decalHit, _rayLenth, _decalsLayer);
        if (decalHit.collider)
        {
            feet.canSpawnDecal = _decalSpawnDuration;
            feet.lastBloodMixe = decalHit.collider.GetComponent<DecalProjector>().material.GetFloat("_Mix");
        }

        Physics.Raycast(feet.pivot.position, feet.pivot.up, out RaycastHit groundHit, _rayLenth, _groundLayer);
        if (!groundHit.collider) feet.asStep = true;


        if (groundHit.collider && feet.canSpawnDecal > 0 && feet.asStep)
        {
            feet.asStep = false;
            OnStepEvent.Invoke(groundHit);
            DecalProjector newDecal = Instantiate(_feetDecalPrefab
                                                , groundHit.point + new Vector3(0, .06f, 0)
                                                , Quaternion.LookRotation(-groundHit.normal, transform.forward));

            // identifier setter to get which decal come from Player's feet
            var identifier = newDecal.gameObject.AddComponent<DecalIdentifier>();
            identifier.Type = DecalType.Player;

            newDecal.GetComponent<DecalControler>().SetLifeTime(_decalLifeTime
                                                                , Mathf.InverseLerp(0, _decalSpawnDuration, feet.canSpawnDecal));

            newDecal.material = new Material(newDecal.material);
            newDecal.material.SetTexture("_Decal", feet.texture2D);
            newDecal.material.SetFloat("_Mix", feet.lastBloodMixe);
            return;
        }

        if (groundHit.collider && feet.asStep)
        {
            feet.asStep = false;
            OnStepEvent.Invoke(groundHit);
            return;
        }
    }
}