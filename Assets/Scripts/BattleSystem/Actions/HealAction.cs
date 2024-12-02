using Game.Interfaces;

namespace Game.BattleSystem.Actions
{
    public class HealAction : IBattleAction
    {
        private IHealable _target;
        private int _amount;

        public HealAction(IHealable target, int amount)
        {
            _target = target;
            _amount = amount;
        }
        
        public void Act()
        {
            _target.Heal(_amount);
        }
    }
}