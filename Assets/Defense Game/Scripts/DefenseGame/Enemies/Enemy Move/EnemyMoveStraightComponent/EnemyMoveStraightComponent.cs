using UnityEngine;
using System;
using UsefulObjects;

namespace DefenseGame
{
    public class EnemyMoveStraightComponent : MonoBehaviour, IEnemyMoveComponent, IActivationAffected
    {
        public InitOrder InitAfterActivationOrder => _initAfterActivationOrder;
        public float SpeedProportion
        {
            get
            {
                return _speed / _baseSpeed;
            }
        }

        [SerializeField] private InitOrder _initAfterActivationOrder;

        [Header("Direction")]
        [SerializeField] private MoveDirection _moveDirection;

        [Header("Speed")]
        [SerializeField] private float _baseSpeed;
        [SerializeField] private float _maxSpeedVarValue;

        private float _speed;
        private TrueFlagService _stopMove;

        private EnemyComponent _enemy;
        private Transform _transform;

        public void InitializeAfterActivation()
        {
            UpdateRandomizedValues();
        }

        private void UpdateRandomizedValues()
        {
            _speed = _baseSpeed + UnityEngine.Random.Range(-_maxSpeedVarValue, _maxSpeedVarValue);
        }

        public void AddStopRequest()
        {
            _stopMove.AddTrueRequest();
        }

        public void RemoveStopRequest()
        {
            _stopMove.RemoveTrueRequest();
        }

        private void Awake()
        {
            _stopMove = new TrueFlagService();
            UpdateRandomizedValues();

            _enemy = GetComponent<EnemyComponent>();
            _transform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            int k = _moveDirection == MoveDirection.Right ? 1 : -1;

            if (!_stopMove.Flag)
            {
                float xAdd = _speed * Time.fixedDeltaTime * k;

                _enemy.MovePosition(new Vector2(_transform.position.x + xAdd, _transform.position.y));
            }

            _transform.localScale = new Vector2(Math.Abs(_transform.localScale.x) * k,
                _transform.localScale.y);
        }

        private void OnDisable()
        {
            _stopMove.DeleteRequestInfo();
        }
    }
}