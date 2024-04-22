using System;


namespace DefenseGame
{
    public interface IEnemyHealthComponent
    {
        public delegate void OnHitHandler(DamageType damageType, float damage);

        public event Action onHealthIsOver;
        public event OnHitHandler onHit;

        public float BaseHealth { get; }
        public bool IsAlive { get; }
    }
}