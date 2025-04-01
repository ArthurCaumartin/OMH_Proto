 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EssenceAspiration : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private bool _active;
    [SerializeField] int _index = 1;
    public float _vitesseAspiration = 0;
    [SerializeField] private float _speed;
    private Renderer _renderer;
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        //_essence.SetFloat("_aspiration", 0);
        _renderer.materials[0].SetFloat ("_aspiration", 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (_spawnManager._defenseAsStarted || _active)
        {
            _vitesseAspiration += Time.deltaTime * _speed;
            _vitesseAspiration = Mathf.Clamp(_vitesseAspiration, 0, 1);
            _renderer.materials[0].SetFloat ("_aspiration", _vitesseAspiration);
            // print(_vitesseAspiration);
        }
    }
}
