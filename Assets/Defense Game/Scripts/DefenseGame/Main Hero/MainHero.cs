using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;


namespace DefenseGame
{
    public class MainHero : MonoBehaviour, IPlayerCharacter, IControllable
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform _weaponMoverTransform;

        public Vector3 Position 
        {
            get
            {
                return _transform.position;
            }
            set
            {
                _transform.position = value;
            }
        }
        
        public float X => _transform.position.x;
        public float Y => _transform.position.y;
        
        private Transform _transform;
        private Rigidbody2D _rb;
        private Weapon _weapon;

        public void MoveUpDown(float moveYDirection)
        {
            _rb.MovePosition(new Vector2(X, Y + _speed * Time.fixedDeltaTime * moveYDirection));
        }

        public Weapon SwitchWeapon(Weapon newWeapon)
        {
            var previousWeapon = _weapon;

            _weapon = newWeapon;
            _weapon.transform.SetParent(_weaponMoverTransform);
            _weapon.transform.position = _weaponMoverTransform.position;

            return previousWeapon;
        }

        public Weapon TakeWeaponAway()
        {
             var weaponResult = _weapon;

            _weapon = null;

            return weaponResult;
        }

        public void Attack()
        {
            _weapon.TryAttack();
        }

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rb = GetComponent<Rigidbody2D>();
        }
    }
}

