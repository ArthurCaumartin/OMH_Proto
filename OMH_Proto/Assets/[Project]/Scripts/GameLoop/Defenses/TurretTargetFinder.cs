using System.Collections.Generic;
using UnityEngine;

public class TurretTargetFinder : MonoBehaviour
{
    public bool DEBUG = true;
    [SerializeField] private LayerMask _mobLayer;
    private List<EnemyLife> _mobInRangeList = new List<EnemyLife>();
    public float _range;

    public EnemyLife GetNearsetMob()
    {
        _mobInRangeList = GetAllMobInRange();
        // print(_mobInRangeList.Count + " mob in range");
        if (_mobInRangeList.Count == 0) return null;

        EnemyLife toReturn = null;

        float minDistance = Mathf.Infinity;
        foreach (var item in _mobInRangeList)
        {
            RaycastHit[] hit = Physics.RaycastAll(transform.position, item.transform.position - transform.position, _range);

            if (hit[0].collider.gameObject.layer != 15)
            {
                if (!item) continue;
                float currentDistance = (item.transform.position - transform.position).sqrMagnitude;
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    toReturn = item;
                }
            }
        }
        return toReturn;
    }

    private void DrawRay(Vector3 origin, Vector3 direction, float maxDistance)
    {
        Debug.DrawRay(origin, direction * maxDistance, Color.red);
    }

    private List<EnemyLife> GetAllMobInRange()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _range, _mobLayer);
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

    public void OnDrawGizmos()
    {
        if(!DEBUG) return;
        Gizmos.color = new Color(0, 0, 1, .2f);
        Gizmos.DrawSphere(transform.position, _range);
    }
}
