using UnityEngine;
using UsefulObjects;


namespace DefenseGame
{
    public class AnimationController : MonoBehaviour
    {
        protected Animator Animator => _animator;
        protected bool IsOnStop => _isOnStop.Flag;

        private Animator _animator;
        private TrueFlagService _isOnStop;

        public virtual void AddStopAnimatorRequest()
        {
            _isOnStop.AddTrueRequest();

            _animator.speed = 0;
        }

        public virtual void RemoveStopAnimatorRequest()
        {
            _isOnStop.RemoveTrueRequest();

            if (!_isOnStop.Flag)
                _animator.speed = 1;
        }

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            _isOnStop = new TrueFlagService();
        }
    }
}