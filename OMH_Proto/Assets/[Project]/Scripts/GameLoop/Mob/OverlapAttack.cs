using UnityEngine;

public class OverlapAttack : MonoBehaviour
{
    [SerializeField] private Transform _pivot;
    [SerializeField] private FloatReference _damage;
    [SerializeField] private FloatReference _raduis;

    public void Attack()
    {
        Collider[] col = Physics.OverlapSphere(_pivot.position, _raduis.Value);
        for (int i = 0; i < col.Length; i++)
        {
            IDamageable overLapHealth = col[i].GetComponent<IDamageable>();
            if (overLapHealth != null)
            {
                overLapHealth.TakeDamages(_damage.Value);
                // overLapHealth.health.Value -= _damage.Value;
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawSphere(_pivot.position, _raduis.Value);
    }
}