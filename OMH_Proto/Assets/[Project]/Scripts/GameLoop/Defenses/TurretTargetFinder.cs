using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargetFinder : MonoBehaviour
{
    [SerializeField] private List<EnemyLife> _mobInRangeList = new List<EnemyLife>();
    public float _range;
    private SphereCollider _collider;
    private StatContainer _stat;
    private TurretCannon _cannon;

    private void OnValidate()
    {
        GetComponent<SphereCollider>().radius = _range;
    }

    public EnemyLife GetNearsetMob()
    {
        if (_mobInRangeList.Count == 0) return null;

        EnemyLife toReturn = null;

        float minDistance = Mathf.Infinity;
        foreach (var item in _mobInRangeList)
        {
            if (!item) continue; 
            float currentDistance = (item.transform.position - transform.position).sqrMagnitude;
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                toReturn = item;
            }
        }
        return toReturn;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyLife mob = other.GetComponent<EnemyLife>();
        if (mob)
        {
            mob.OnDeathEvent.AddListener(RemoveMob);
            _mobInRangeList.Add(mob);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyLife mob = other.GetComponent<EnemyLife>();
        if (mob && _mobInRangeList.Contains(mob))
        {
            _mobInRangeList.Remove(mob);
        }
    }

    public void RemoveMob(EnemyLife toRemove)
    {
        if (_mobInRangeList.Contains(toRemove))
            _mobInRangeList.Remove(toRemove);
    }
}
