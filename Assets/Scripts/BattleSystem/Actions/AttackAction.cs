using System.Collections;
using System;
using Game.Interfaces;

namespace Game.BattleSystem.Actions
{
    public class AttackAction<T> : IBattleAction where T : IDamageable, IMoveable 
    {
        private IAttackable _attacker;
        private T _target;
        
        public AttackAction(IAttackable attacker, T target)
        {
            _attacker = attacker;
            _target = target;
        }
        
        public void Act()
        {
            _attacker.Attack(_target);
        }
    }
}