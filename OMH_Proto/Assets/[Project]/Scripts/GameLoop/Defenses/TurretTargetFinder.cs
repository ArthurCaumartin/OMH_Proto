using System.Collections.Generic;
using UnityEngine;

public class TurretTargetFinder : MonoBehaviour
{
    public bool DEBUG = true;
    [SerializeField] private LayerMask _mobLayer;
    private List<EnemyLife> _mobInRangeList = new List<EnemyLife>();

    public EnemyLife GetNearsetMob(float range)
    {
        _mobInRangeList = GetAllMobInRange(range);
        // print(_mobInRangeList.Count + " mob in range");
        if (_mobInRangeList.Count == 0) return null;

        EnemyLife toReturn = null;
        float minDistance = Mathf.Infinity;

        foreach (var item in _mobInRangeList)
        {
            if (!item) continue;

            RaycastHit[] hits = Physics.RaycastAll(transform.position, item.transform.position - transform.position, range);
            Debug.DrawRay(transform.position, item.transform.position - transform.position, Color.green);
            if (hits.Length > 0 && hits[0].collider.gameObject.layer == 15) continue;

            float currentDistance = (item.transform.position - transform.position).sqrMagnitude;
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

    private List<EnemyLife> GetAllMobInRange(float range)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range, _mobLayer);
        List<EnemyLife> mobInRange = new List<EnemyLife>();
        for (int i = 0; i < hits.Length; i++)
        {
            EnemyLife e = hits[i].GetComponent<EnemyLife>();
            if (e) mobInRange.Add(e);
        }
        return mobInRange;
    }

    public void RemoveMob(EnemyLife toRemove)
    {
        if (_mobInRangeList.Contains(toRemove))
            _mobInRangeList.Remove(toRemove);
    }
}