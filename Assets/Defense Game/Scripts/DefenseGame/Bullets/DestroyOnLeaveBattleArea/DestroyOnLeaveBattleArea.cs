using UnityEngine;


namespace DefenseGame
{
    public class DestroyOnLeaveBattleArea : MonoBehaviour
    {
        private BulletBattleAreaDetector _battleAreaDetector;
        private BulletComponent _bullet;

        private void OnLiveCycleWasStarted()
        {
            _battleAreaDetector.SwitchOn();
            if (!_battleAreaDetector.IsOnBattleAreaInstantVar)
            {
                _bullet.Die();
            }   
        }

        private void OnExitBattleArea()
        {
            _bullet.Die();
        }

        private void Awake()
        {
            _battleAreaDetector = GetComponent<BulletBattleAreaDetector>();
            _bullet = GetComponent<BulletComponent>();
        }

        private void OnEnable()
        {
            _battleAreaDetector.onExitBattleArea += OnExitBattleArea;
            _bullet.onLiveCycleWasStarted += OnLiveCycleWasStarted;
        }

        private void OnDisable()
        {
            _battleAreaDetector.onExitBattleArea -= OnExitBattleArea;
            _bullet.onLiveCycleWasStarted -= OnLiveCycleWasStarted;
        }
    }
}