using UnityEngine;
using System;



namespace DefenseGame
{
    public abstract class BulletComponent : ComplexInitPoolableComponent
    {
        public delegate void OnHitHandler(Collider2D other);
        public event Action onDeath;
        public event OnHitHandler onHit;
        public event Action onLiveCycleWasStarted;

        public Vector2 Position
        {
            get
            {
                return Transform.position;
            }
            set
            {
                Transform.position = value;
            }
        }

        public void Die()
        {
            onDeath?.Invoke();
            BackToPool();
        }

        public void StartLiveCycle()
        {
            onLiveCycleWasStarted?.Invoke();
        }

        abstract protected bool CanHit(Collider2D other);

        abstract protected void Hit(Collider2D other);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (CanHit(other))
            {
                onHit?.Invoke(other);
                Hit(other);
            }
        }
    }
}