using Unity.VisualScripting;
using UnityEngine;

namespace DefenseGame
{
    public class MainHeroAnimationController : MonoBehaviour
    {
        [SerializeField] private string _moveFlagName;

        private Animator _animator;
        private CharacterController _characterController;
        private bool _isCharacterMoving;

        private void OnMoveStarted()
        {
            _isCharacterMoving = true;
            _animator.SetBool(_moveFlagName, true);
        }

        private void OnMoveStopped()
        {
            _isCharacterMoving = false;
        }

        public void TryChangeToIdle()
        {
            if (!_isCharacterMoving)
                _animator.SetBool(_moveFlagName, false);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
            _isCharacterMoving = false;
        }

        private void OnEnable()
        {
            _characterController.onMoveStarted += OnMoveStarted;
            _characterController.onMoveStopped += OnMoveStopped;
        }

        private void OnDisable()
        {
            _characterController.onMoveStarted -= OnMoveStarted;
            _characterController.onMoveStopped -= OnMoveStopped;
        }
    }
}