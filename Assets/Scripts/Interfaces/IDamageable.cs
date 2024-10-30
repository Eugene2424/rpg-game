using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public bool IsDead { get; set; }
    public void GetHit(int damage);
    public void Dead();
}
