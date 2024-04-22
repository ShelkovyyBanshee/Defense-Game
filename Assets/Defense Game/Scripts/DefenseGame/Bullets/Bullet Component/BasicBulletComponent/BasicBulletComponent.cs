using UnityEngine;

namespace DefenseGame
{
    public class BasicBulletComponent : BulletComponent
    {
        public string TargetTag => _targetTag;

        [Header("Target")]
        [SerializeField] string _targetTag;

        [Header("Damage")]
        [SerializeField] float _damage;
        [SerializeField] DamageType _damageType;

        [Header("Collisions")]
        [SerializeField] bool _infiniteCollisions;
        private bool _wasCollision;
        private Collider2D _collider;

        protected override void ExecuteAfterActivationAdditional()
        {
            _wasCollision = false;
        }

        protected override bool CanHit(Collider2D other)
        {
            return !_wasCollision && other.CompareTag(_targetTag);
        }

        protected override void Hit(Collider2D other)
        {
            var damageable = other.GetComponent<IDamageable>();

            if (!damageable.IsAlive) 
                return;

            damageable.ApplyDamage(_damage, _damageType);

            if (!_infiniteCollisions)
            {
                _wasCollision = true;
                Die();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<Collider2D>();    
        }

        private void OnDisable()
        {
            _collider.enabled = true;
        }
    }
}