using System;
using UnityEngine;


namespace DefenseGame
{
    public class BasicEnemyHealthComponent : MonoBehaviour, IDamageable, IEnemyHealthComponent, IActivationAffected
    {
        public event Action onHealthIsOver;
        public event IEnemyHealthComponent.OnHitHandler onHit;

        public InitOrder InitAfterActivationOrder => _initAfterActivationOrder;
        public bool IsAlive => _health > 0;
        public float BaseHealth => _maxHealth;

        protected float Health
        {
            get
            {
                return _health;
            }
            set
            {
                if (value < 0)
                    _health = 0;
                else if (value > _maxHealth)
                    _health = _maxHealth;
                else
                    _health = value;
            }
        }

        [SerializeField] private InitOrder _initAfterActivationOrder;

        [Header("Health properties")]
        [SerializeField] private float _maxHealth;

        private float _health;
        private EnemyComponent _enemy;

        public void InitializeAfterActivation()
        {
            _health = _maxHealth;
        }

        public void ApplyDamage(float damage, DamageType damageType = DamageType.NoType)
        {
            ProcessDamage(damage, damageType);

            if (CheckHealth())
            {
                onHealthIsOver?.Invoke();
            }
        }

        protected virtual bool CheckHealth()
        {
            return _health == 0;
        }

        protected virtual void ProcessDamage(float damage, DamageType damageType)
        {
            Health -= damage;
            ApplyHit(damageType, damage);
        }

        protected void ApplyHit(DamageType damageType, float damage)
        {
            onHit?.Invoke(damageType, damage);
        }

        private void Awake()
        {
            _enemy = GetComponent<EnemyComponent>();
            _health = _maxHealth;
        }
    }
}