using UnityEngine;

public class Health : MonoBehaviour
{
    public FloatReference health;

    void Update()
    {
        if(health.Value <= 0) Destroy(gameObject);
    }
}
