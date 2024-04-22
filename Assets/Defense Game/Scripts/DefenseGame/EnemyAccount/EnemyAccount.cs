using UnityEngine;

using System.Collections.Generic;


namespace DefenseGame
{
    public class EnemyAccount : MonoBehaviour, IActivationAffected
    {
        private List<EnemyCounter> _counters;
        private IEnemyDeathController _healthHandler;

        public InitOrder InitAfterActivationOrder => InitOrder.FirstOrder;

        public void AddCounter(EnemyCounter counter)
        {
            if (_counters == null)
            {
                _counters = new List<EnemyCounter>();
            }

            _counters.Add(counter);
        }

        public void InitializeAfterActivation()
        {
            foreach(var counter in _counters)
            {
                counter.CountEnemy();
            }
        }

        private void OnEnemyWasDefeated()
        {
            foreach (var counter in _counters)
            {
                counter.DiscountEnemy();
            }
        }

        private void Awake()
        {
            _healthHandler = GetComponent<IEnemyDeathController>();   
        }

        private void OnEnable()
        {
            _healthHandler.onWasDefeated += OnEnemyWasDefeated;
        }

        private void OnDisable()
        {
            _healthHandler.onWasDefeated -= OnEnemyWasDefeated;
            if (!_healthHandler.IsDefeated)
            {
                foreach (var counter in _counters)
                {
                    counter.DiscountEnemy();
                }
            }
            
        }
    }
}