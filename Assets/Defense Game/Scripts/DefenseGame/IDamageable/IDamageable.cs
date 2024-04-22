

namespace DefenseGame
{
    public interface IDamageable
    {
        public bool IsAlive { get; }

        public void ApplyDamage(float damage, DamageType damageType=DamageType.NoType);
    }
}