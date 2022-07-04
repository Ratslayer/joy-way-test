using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Transform _muzzleTransform;
    [SerializeField]
    private AbstractProjectile _projectilePrefab;
    public event Action Unequipped;
    public void Unequip() => Unequipped?.Invoke();
    public void Attack()
    {
        var projectile = Instantiate(_projectilePrefab);
        projectile.Shoot(_muzzleTransform);
    }
}