using UnityEngine;


namespace DefenseGame
{
    public class EnemyBattleAreaDetector : BattleAreaDetector
    {
        private IEnemyDeathController _healthHandler;

        private void OnEnemyWasDefeated()
        {
            BACollider.Disable();
        }

        protected override void Awake()
        {
            base.Awake();
            _healthHandler = GetComponent<IEnemyDeathController>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _healthHandler.onWasDefeated += OnEnemyWasDefeated;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _healthHandler.onWasDefeated -= OnEnemyWasDefeated;
        }
    }
}