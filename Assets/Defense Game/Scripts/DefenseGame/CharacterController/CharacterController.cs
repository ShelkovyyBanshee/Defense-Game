using Newtonsoft.Json.Linq;
using UnityEngine;

namespace DefenseGame
{
    public class CharacterController : MonoBehaviour
    {
        public delegate void OnMoveChangedEventHandler();

        public event OnMoveChangedEventHandler onMoveStarted;
        public event OnMoveChangedEventHandler onMoveStopped;

        private ICharacterInput _input;
        private IControllable _controllable;
        private WeaponManager _weaponManager;

        private bool _onDelayedAttack;

        private bool _onDelayedSwitch;
        private int _delayedSwitchMove;

        private bool _isMoving;

        public void SetWeapons(Weapon[] weapons)
        {
            _weaponManager.UpdateWeapons(weapons);

            _weaponManager.CurrentWeapon.onAttackEnds += OnAttackEnds;
        }

        public void ClearWeapons()
        {
            if (_weaponManager.CurrentWeapon != null)
                _weaponManager.CurrentWeapon.onAttackEnds -= OnAttackEnds;

            _weaponManager.DestroyOldWeapons();
        }

        private void OnInputUpdate()
        {
            MoveUpdate();
            WeaponSwitchUpdate();
            AttackUpdate();
        }

        private void SwitchIsMovingFlag()
        {
            _isMoving = !_isMoving;

            if (_isMoving)
                onMoveStarted?.Invoke();
            else
                onMoveStopped?.Invoke();
        }

        private void MoveUpdate()
        {
            var sumMoveDirection = 0.0f;
            sumMoveDirection += _input.IsMovingUpInFrame ? 1 : 0;
            sumMoveDirection += _input.IsMovingDownInFrame ? -1 : 0;

            if ((sumMoveDirection != 0) != _isMoving)
                SwitchIsMovingFlag();
        }

        private void AttackUpdate()
        {
            if (_input.IsAttackingInFrame)
            {
                if (!_weaponManager.CurrentWeapon.IsLocked)
                    _controllable.Attack();
                else if (!_input.IsAutomaticAttackModeOn)
                {
                    _onDelayedAttack = true;
                }
            }
        }

        private void WeaponSwitchUpdate()
        {
            if (_input.IsSwitchingToPreviousInFrame != _input.IsSwitchingToNextInFrame)
            {
                if (!_weaponManager.CurrentWeapon.IsLocked)
                {
                    _weaponManager.CurrentWeapon.onAttackEnds -= OnAttackEnds;

                    if (_input.IsSwitchingToNextInFrame)
                        _weaponManager.SwitchToNextWeapon(true);
                    else
                        _weaponManager.SwitchToPreviousWeapon(true);

                    _weaponManager.CurrentWeapon.onAttackEnds += OnAttackEnds;
                }
                else
                {
                    _onDelayedSwitch = true;
                    _delayedSwitchMove += _input.IsSwitchingToNextInFrame ? 1 : -1;
                }
            }
        }

        private void OnAttackEnds()
        {
            if (_onDelayedAttack && !_onDelayedSwitch)
            {
                _controllable.Attack();
            }
            else if (_onDelayedSwitch && !_onDelayedAttack)
            {
                _weaponManager.CurrentWeapon.onAttackEnds -= OnAttackEnds;

                if (_input.IsAutomaticAttackModeOn)
                {
                    _weaponManager.SwitchByMove(_delayedSwitchMove, false);
                    _controllable.Attack();
                }
                else
                    _weaponManager.SwitchByMove(_delayedSwitchMove, true);

                _weaponManager.CurrentWeapon.onAttackEnds += OnAttackEnds;
            }
            else if (_onDelayedAttack && _onDelayedSwitch)
            {
                _weaponManager.CurrentWeapon.onAttackEnds -= OnAttackEnds;

                _weaponManager.SwitchByMove(_delayedSwitchMove, false);
                _controllable.Attack();

                _weaponManager.CurrentWeapon.onAttackEnds += OnAttackEnds;
            }

            if (!_onDelayedAttack && !_onDelayedSwitch && _input.IsAutomaticAttackModeOn)
                _controllable.Attack();

            _onDelayedAttack = false;
            _onDelayedSwitch = false;
            _delayedSwitchMove = 0;
        }

        private void Update()
        {
            if (_isMoving)
            {
                var sumMoveDirection = 0.0f;
                sumMoveDirection += _input.IsMovingUpInFrame ? 1 : 0;
                sumMoveDirection += _input.IsMovingDownInFrame ? -1 : 0;

                if (sumMoveDirection != 0)
                    _controllable.MoveUpDown(sumMoveDirection);
            }
        }

        private void Awake()
        {
            _controllable = GetComponent<IControllable>();
            _input = GetComponent<ICharacterInput>();

            var weaponsDirectory = (new GameObject("[INACTIVE WEAPONS]")).transform;
            weaponsDirectory.SetParent(transform);

            _weaponManager = new WeaponManager(_controllable, weaponsDirectory);
        }

        private void OnEnable()
        {
            _input.onInputUpdate += OnInputUpdate;
            _isMoving = false;
            _onDelayedAttack = false;
            _onDelayedSwitch = false;
            _delayedSwitchMove = 0;
        }

        private void OnDisable()
        {
            _input.onInputUpdate -= OnInputUpdate;
            _weaponManager.CurrentWeapon.onAttackEnds -= OnAttackEnds;
        }
    }
}