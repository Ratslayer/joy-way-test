using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    public event Action Unequipped;
    public void Unequip() => Unequipped?.Invoke();
}
