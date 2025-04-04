using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamages(GameObject damageDealer, float damageAmount, DamageType type = DamageType.Unassigned);
}
