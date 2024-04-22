using UnityEngine;
using System;


namespace DefenseGame
{
    public class BasicEnemyDeathController : MonoBehaviour, IEnemyDeathController
    {
        public event Action onWasDefeated;

        public bool IsDefeated => _enemyHealth.IsAlive;

        private IEnemyHealthComponent _enemyHealth;

        private void OnHealthIsOver()
        {
            onWasDefeated?.Invoke();
        }

        private void Awake()
        {
            _enemyHealth = GetComponent<IEnemyHealthComponent>();
        }

        private void OnEnable()
        {
            _enemyHealth.onHealthIsOver += OnHealthIsOver;
        }

        private void OnDisable()
        {
            _enemyHealth.onHealthIsOver -= OnHealthIsOver;
        }
    }
}