namespace Game.Interfaces
{
    public interface IAttackable
    {
        public void Attack<T>(T target) where T : IDamageable, IMoveable;
    }
}