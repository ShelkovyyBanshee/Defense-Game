using UnityEditor;
using UnityEngine;
using System;


namespace DefenseGame
{
    public class PixelPerfectOffset : MonoBehaviour
    {
        [SerializeField] private Transform _mainTransform;

        [SerializeField] private float _offsetX;
        [SerializeField] private float _offsetY;

        [SerializeField] private float _step;

        private Transform _transform;
        private float _actualOffsetX;
        private float _actualOffsetY;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (Math.Abs(_actualOffsetX - _offsetX) >= _step)
            {
                _actualOffsetX = _offsetX;
                transform.position = new Vector2(_mainTransform.position.x + _offsetX,
                    _mainTransform.position.y);
            }

            if (Math.Abs(_actualOffsetY - _offsetY) >= _step)
            {
                _actualOffsetY = _offsetY;
                transform.position = new Vector2(_mainTransform.position.x, 
                    _mainTransform.position.y + _offsetY);
            }
        }
    }
}