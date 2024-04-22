using UnityEngine;
using System;

namespace DefenseGame
{
    public class EnemyComponent : ComplexInitPoolableComponent
    {
        public event Action onPositionChanged;
        public event Action onPositionChangedInstantly;

        public bool IsAccountable => _isAccountable;
        public EnemyGenus EnemyGenus => _enemyGenus;

        [SerializeField] private EnemyGenus _enemyGenus;
        [SerializeField] private EnemyFraction _fraction;
        [SerializeField] private bool _isAccountable;
        
        private Rigidbody2D _rb;

        public void InstantlyMovePosition(Vector2 position)
        {
            Transform.position = position;
            onPositionChangedInstantly?.Invoke();
            onPositionChanged?.Invoke();
        }

        public void MovePosition(Vector2 position)
        {
            _rb.MovePosition(position);
            onPositionChanged?.Invoke();
        }

        public void Disappear()
        {
            BackToPool();
        }

        protected override void AwakeAdditional()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
    }
}