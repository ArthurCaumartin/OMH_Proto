using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatrolPoints : MonoBehaviour
{
    public static PatrolPoints instance;
    public bool DEBUG = true; 
    private List<Transform> _transformList = new List<Transform>();

    private void Awake()
    {
        if (instance) Destroy(gameObject);
        instance = this;
    }

    private void Start()
    {
        _transformList = transform.GetComponentsInChildren<Transform>().ToList();
        if (_transformList.Contains(transform)) _transformList.Remove(transform);
    }

    public Vector3 GetRandomPoint(float precision)
    {
        return _transformList[Random.Range(0, _transformList.Count)].position + (Random.insideUnitSphere.normalized * precision);
    }

    public Vector3 GetFarsetPoint(Vector3 position, float precision)
    {
        float maxDistance = 0;
        Vector3 posToReturn = position;

        for (int i = 0; i < _transformList.Count; i++)
        {
            float currentDis = Vector3.Distance(position, _transformList[i].position);
            if (currentDis > maxDistance)
            {
                maxDistance = currentDis;
                posToReturn = _transformList[i].position;
            }
        }

        return posToReturn + Random.insideUnitSphere * precision;
    }

    private void OnDrawGizmos()
    {
        if(!DEBUG) return;
        Gizmos.color = new Color(0, 1, 0, .1f);
        foreach (var item in transform.GetComponentsInChildren<Transform>())
        {
            if (item == transform) continue;
            Gizmos.DrawSphere(item.position, .5f);
        }
    }
}
