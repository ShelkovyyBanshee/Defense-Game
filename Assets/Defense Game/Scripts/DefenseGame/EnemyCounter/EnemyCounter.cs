using System;
using UnityEngine;

namespace DefenseGame
{
    public class EnemyCounter
    {
        public event Action onNoEnemiesForNow;
        public event Action onNewEnemyAppeared;
        public event Action onAnotherEnemyDefeated;

        public int ActiveEnemiesCount => _activeEnemiesCount;
        public bool NoActiveEnemies => _activeEnemiesCount == 0; 

        private int _activeEnemiesCount;
        
        public void CountEnemy()
        {
            _activeEnemiesCount += 1;
            onNewEnemyAppeared?.Invoke();
        }

        public void DiscountEnemy()
        {
            _activeEnemiesCount -= 1;
            onAnotherEnemyDefeated?.Invoke();
            if (_activeEnemiesCount == 0)
                onNoEnemiesForNow?.Invoke();
        }
    }
}