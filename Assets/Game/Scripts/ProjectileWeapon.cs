using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProjectileWeapon : AbstractWeapon
{
    [SerializeField]
    private Transform _muzzleTransform;
    [SerializeField]
    private AbstractProjectile _projectilePrefab;
    public override void BeginAttack()
    {
        base.BeginAttack();
        var projectile = Instantiate(_projectilePrefab);
        projectile.Shoot(_muzzleTransform);
    }
}
