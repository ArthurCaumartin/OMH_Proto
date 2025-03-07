using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretTargetFinder : MonoBehaviour
{
    [SerializeField] private LayerMask _mobLayer;
    [SerializeField] private LayerMask _wallLayer;
    private List<MobLife> _mobInRangeList = new List<MobLife>();

    public MobLife GetNearsetMob(float range)
    {
        _mobInRangeList = GetAllMobInRange(range);
        // print(_mobInRangeList.Count + " mob in range");
        if (_mobInRangeList.Count == 0) return null;

        MobLife toReturn = null;
        float minDistance = Mathf.Infinity;

        foreach (var item in _mobInRangeList)
        {
            if (!item) continue;
            if (IsBehindWall(item.transform)) continue;

            float currentDistance = Vector3.Distance(item.transform.position, transform.position);
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                toReturn = item;
            }
        }
        // print(toReturn ? "Return target" : "Nothing to return");
        return toReturn;
    }

    private void DrawRay(Vector3 origin, Vector3 direction, float maxDistance)
    {
        Debug.DrawRay(origin, direction * maxDistance, Color.red);
    }

    private List<MobLife> GetAllMobInRange(float range)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range, _mobLayer);
        List<MobLife> mobInRange = new List<MobLife>();
        for (int i = 0; i < hits.Length; i++)
        {
            MobLife e = hits[i].GetComponent<MobLife>();
            if (e) mobInRange.Add(e);
        }
        return mobInRange;
    }

    public void RemoveMob(MobLife toRemove)
    {
        if (_mobInRangeList.Contains(toRemove))
            _mobInRangeList.Remove(toRemove);
    }

    private bool IsBehindWall(Transform obj)
    {
        return Physics.Linecast(transform.position, obj.position, _wallLayer);
    }
}