using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IDamageable
    {
        public void GetHit(int damage);
        public void Dead();
    }   
}
