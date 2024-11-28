using UnityEngine;

public class Gun : Weapon
{
    [Space]
    [SerializeField] private FloatReference _bulletCount;
    [SerializeField] private FloatReference _spread;
    [SerializeField] private FloatReference _randomness;

    public override void Attack()
    {
        for (int i = 0; i < (int)_bulletCount.Value; i++)
        {
            Projectile newProj = Instantiate(_projectile, transform.position, transform.rotation);
            newProj.Initialize(_stat.projectileSpeed.Value, _stat.damage.Value);
            if (_bulletCount.Value == 1) return;

            float countTime = Mathf.InverseLerp(0, _bulletCount.Value - 1, i);
            Vector3 newOrientation = new Vector3(Mathf.Lerp(-_spread.Value, _spread.Value, countTime), 0, 1);

            if (_randomness.Value > 0)
            {
                newOrientation = Vector3.Lerp(newOrientation, Random.insideUnitSphere, _randomness.Value);
                newOrientation.y = 0;
            }

            newOrientation = newOrientation.normalized;
            newProj.transform.forward = transform.rotation * newOrientation;
        }
    }

    public override void SecondaryAttack()
    {
        



    }
}
