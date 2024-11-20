using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalesSpawner : MonoBehaviour
{
    [SerializeField] private DecalProjector _decalePrefab;
    private EnemyLife _mobLife;

    private void Start()
    {
        _mobLife = GetComponent<EnemyLife>();
        _mobLife.OnDamageEvent.AddListener(SpawnDecals);
    }

    private void SpawnDecals()
    {
        Vector3 offset = Random.insideUnitSphere;
        offset.y = 0;
        DecalProjector d = Instantiate(_decalePrefab, transform.position + offset, Quaternion.Euler(90f, 0, Random.Range(0, 360)));
        d.transform.position = new Vector3(d.transform.position.x, 6, d.transform.position.z);
        // Destroy(d.gameObject, 5);
    }
}
