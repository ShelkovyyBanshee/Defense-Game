using UnityEngine;
using System;
using UsefulObjects;


namespace DefenseGame
{
    public class BulletMoveStraightComponent : MonoBehaviour, IBulletMoveComponent
    {
        public float DistanceDelta
        {
            get
            {
                return !_stopMove.Flag ? Time.fixedDeltaTime * _speed : 0;
            }
        }
        public bool IsMoving => !_stopMove.Flag;

        [SerializeField] private MoveDirection _moveDirection;
        [SerializeField] private float _speed;

        private TrueFlagService _stopMove;
        private Rigidbody2D _rb;
        private Transform _transform;

        public void AddStopRequest()
        {
            _stopMove.AddTrueRequest();
        }

        public void RemoveStopRequest()
        {
            _stopMove.RemoveTrueRequest();
        }

        private void FixedUpdate()
        {
            if (!_stopMove.Flag)
            {
                int k = _moveDirection == MoveDirection.Left ? -1 : 1;
                float xAdd = Time.fixedDeltaTime * _speed * k;

                _rb.MovePosition(new Vector2(_transform.position.x + xAdd, _transform.position.y));

                _transform.localScale = new Vector2(Math.Abs(_transform.localScale.x) * k * -1,
                    transform.localScale.y);
            }
        }

        private void Awake()
        {
            _stopMove = new TrueFlagService();
            _rb = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
        }

        private void OnDisable()
        {
            _stopMove.DeleteRequestInfo();
        }
    }
}