using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnDefenseTime : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private GameObject MovingObject;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private FloatReference _defenseDuration;
    [SerializeField] private bool Active;
    private float _timePassed;
    // Start is called before the first frame update
    // void Start()
    // {
    //     PositionInitiale = MovingObject.transform.position;
    // }

    // Update is called once per frame
    void Update()
    {
        if(_spawnManager._defenseAsStarted || Active) ObjectMovement();
    }

    public void ObjectMovement()
    {
        _timePassed += Time.deltaTime;
        MovingObject.transform.position = Vector3.Lerp(startPos.position, endPos.position, Mathf.InverseLerp(0, _defenseDuration.Value, _timePassed));
    }
}
