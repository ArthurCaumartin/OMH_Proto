using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostBackup : MonoBehaviour
{
    public float costBackup;

    public float GetCostOnHealth()
    {
        Health h = GetComponent<Health>();
        float healthRatio = h ? h.GetHealtRatio() : 1;
        return costBackup * Mathf.Lerp(0, .8f, healthRatio);
    }
}
