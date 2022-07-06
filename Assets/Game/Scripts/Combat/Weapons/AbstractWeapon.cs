using System;
using UnityEngine;

public class AbstractWeapon : MonoBehaviour
{
    public event Action Unequipped;
    public void Unequip()
    {
        EndAttack();
        Unequipped?.Invoke();
    }
    public virtual void BeginAttack() { }
    public virtual void EndAttack() { }
}
