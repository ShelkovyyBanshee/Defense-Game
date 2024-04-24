using System;
using UnityEngine;

namespace DefenseGame
{
    public class KeyboardCharacterInput : MonoBehaviour, ICharacterInput
    {
        public event Action onInputUpdate;

        public bool IsMovingUpInFrame => Input.GetKey(_moveUpKey);
        public bool IsMovingDownInFrame => Input.GetKey(_moveDownKey);

        public bool IsSwitchingToPreviousInFrame => Input.GetKeyDown(_switchPreviousKey);
        public bool IsSwitchingToNextInFrame => Input.GetKeyDown(_switchNextKey);
        public bool IsAttackingInFrame => Input.GetKeyDown(_attackKey) || _isAutomaticAttackOn;
        public bool IsSwitchingByIndexInFrame => _isWeaponChosenByIndexInFrame;
        public int ChosenWeaponIndex => _chosenWeaponIndex;

        public bool IsAutomaticAttackModeOn => _isAutomaticAttackOn;

        [SerializeField] private KeyCode _moveUpKey;
        [SerializeField] private KeyCode _moveDownKey;
        [SerializeField] private KeyCode _attackKey;
        [SerializeField] private KeyCode _switchPreviousKey;
        [SerializeField] private KeyCode _switchNextKey;
        [SerializeField] private float _automaticAttackTimeRequired;

        private bool _isAutomaticAttackOn;
        private bool _isAttackKeyPressed;

        private float _attackKeyDownTime;

        private bool _isWeaponChosenByIndexInFrame;
        private int _chosenWeaponIndex;

        private bool IsOnUpdate()
        {
            return Input.GetKeyDown(_attackKey) ||
                Input.GetKeyDown(_switchNextKey) || Input.GetKeyDown(_switchPreviousKey) ||
                Input.GetKeyDown(_moveUpKey) || Input.GetKeyDown(_moveDownKey) ||
                Input.GetKeyUp(_moveUpKey) || Input.GetKeyUp(_moveDownKey) 
                || _isWeaponChosenByIndexInFrame;
        }

        private void ValidateAutomaticAttack()
        {
            if (_isAttackKeyPressed != Input.GetKey(_attackKey))
            {
                _isAttackKeyPressed = !_isAttackKeyPressed;

                if (!_isAttackKeyPressed)
                {
                    _attackKeyDownTime = 0;
                    _isAutomaticAttackOn = false;
                }
            }

            if (_isAttackKeyPressed)
            {
                _attackKeyDownTime += Time.deltaTime;
                if (_attackKeyDownTime >= _automaticAttackTimeRequired && !_isAutomaticAttackOn)
                {
                    _isAutomaticAttackOn = true;
                    onInputUpdate?.Invoke();
                }
            }
        }

        private void ValidateWeaponIndexChoice()
        {
            _isWeaponChosenByIndexInFrame = Input.GetKeyDown(KeyCode.Alpha1) ||
                Input.GetKeyDown(KeyCode.Alpha2) ||
                Input.GetKeyDown(KeyCode.Alpha3) ||
                Input.GetKeyDown(KeyCode.Alpha4) ||
                Input.GetKeyDown(KeyCode.Alpha5) ||
                Input.GetKeyDown(KeyCode.Alpha6) ||
                Input.GetKeyDown(KeyCode.Alpha7) ||
                Input.GetKeyDown(KeyCode.Alpha8) ||
                Input.GetKeyDown(KeyCode.Alpha9);

            if (_isWeaponChosenByIndexInFrame)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (Input.GetKeyDown((KeyCode)((int)KeyCode.Alpha1 + i)))
                    {
                        _chosenWeaponIndex = i;
                        return;
                    }
                }
            }

            _chosenWeaponIndex = -1;
        }

        private void Update()
        {
            ValidateAutomaticAttack();
            ValidateWeaponIndexChoice();

            if (IsOnUpdate())
                onInputUpdate?.Invoke();
        }

        private void OnEnable()
        {
            _isAutomaticAttackOn = false;
            _isAttackKeyPressed = false;
            _isWeaponChosenByIndexInFrame = false;
            _chosenWeaponIndex = -1;
            _attackKeyDownTime = 0;
        }
    }
}