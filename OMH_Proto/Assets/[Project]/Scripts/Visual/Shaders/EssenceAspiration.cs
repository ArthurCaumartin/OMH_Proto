using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceAspiration : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private bool Active;
    [SerializeField] private Material _essence;
    [SerializeField] private float _transitionSpeed;
     private float _vitesseAspiration = 1;
     private float transitionTime;

    void Start()
    {
        _essence.SetFloat("_aspiration", 0);
    }

    // Update is called once per frame
    void Update()
    {
       if(Active == true ) 
       {
        transitionTime += Time.deltaTime*_transitionSpeed;
        if (transitionTime > 1) return;
        AspireEssence(transitionTime);
       }
    }
    public void AspireEssence(float value)
    {
        _essence.SetFloat("_aspiration", _vitesseAspiration);
    }
}
