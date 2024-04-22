using UnityEngine;
using UsefulObjects;
using UnityExpansions;


namespace DefenseGame
{
    public class EnemyColliderController : MonoBehaviour, IColliderController,
        IActivationAffected
    {
        public InitOrder InitAfterActivationOrder => _initAfterActivationOrder;

        [SerializeField] private InitOrder _initAfterActivationOrder;

        private BoxCollider2D _collider;
        private TrueFlagService _turnOffCollider;
        private IEnemyDeathController _healthObserver;
        private EnemyBattleAreaDetector _battleAreaDetector;
        private EnemyComponent _enemy;

        private bool _isOnBattleArea;

        public void AddTurnOffColliderRequest()
        {
            _turnOffCollider.AddTrueRequest();
            _collider.enabled = !_turnOffCollider.Flag;
        }

        public void RemoveTurnOffColliderRequest()
        {
            _turnOffCollider.RemoveTrueRequest();
            _collider.enabled = !_turnOffCollider.Flag;
        }

        public void InitializeAfterActivation()
        {
            ValidateIsOnBattleAreaFlagOnInstantMove();
            _collider.enabled = !_turnOffCollider.Flag;
        }

        private void ValidateIsOnBattleAreaFlagOnEnterExit()
        {
            bool isOnBattleArea = _battleAreaDetector.IsOnBattleAreaEnterExitVar;

            if (_isOnBattleArea != isOnBattleArea)
            {
                _isOnBattleArea = isOnBattleArea;

                if (_isOnBattleArea)
                {
                    RemoveTurnOffColliderRequest();
                }

                else
                {
                    AddTurnOffColliderRequest();
                }
                    
            }
        }

        private void ValidateIsOnBattleAreaFlagOnInstantMove()
        {
            bool isOnBattleArea = _battleAreaDetector.IsOnBattleAreaInstantVar;

            if (_isOnBattleArea != isOnBattleArea)
            {
                _isOnBattleArea = isOnBattleArea;

                if (_isOnBattleArea)
                {
                    RemoveTurnOffColliderRequest();
                }

                else
                {
                    AddTurnOffColliderRequest();
                }

            }
        }

        private void OnEnemyWasDefeated()
        {
            AddTurnOffColliderRequest();
        }

        private void Awake()
        {
            _turnOffCollider = new TrueFlagService();

            _collider = GetComponent<BoxCollider2D>();
            _healthObserver = GetComponent<IEnemyDeathController>();
            _battleAreaDetector = GetComponent<EnemyBattleAreaDetector>();
            _enemy = GetComponent<EnemyComponent>();

            _isOnBattleArea = true;
        }

        private void OnEnable()
        {
            _healthObserver.onWasDefeated += OnEnemyWasDefeated;
            _battleAreaDetector.onEnterBattleArea += ValidateIsOnBattleAreaFlagOnEnterExit;
            _battleAreaDetector.onExitBattleArea += ValidateIsOnBattleAreaFlagOnEnterExit;
            _enemy.onPositionChangedInstantly += ValidateIsOnBattleAreaFlagOnInstantMove;
        }

        private void OnDisable()
        {
            _healthObserver.onWasDefeated -= OnEnemyWasDefeated;
            _battleAreaDetector.onEnterBattleArea -= ValidateIsOnBattleAreaFlagOnEnterExit;
            _battleAreaDetector.onExitBattleArea -= ValidateIsOnBattleAreaFlagOnEnterExit;
            _enemy.onPositionChangedInstantly -= ValidateIsOnBattleAreaFlagOnInstantMove;

            _turnOffCollider.DeleteRequestInfo();
            _isOnBattleArea = true;
        }
    }
}