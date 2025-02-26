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
                //! avoid self damage
                if (overLapHealth == transform.parent.GetComponent<IDamageable>()) continue;
                overLapHealth?.TakeDamages(gameObject, _damage.Value);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(_pivot.position, _raduis.Value);
    }
}