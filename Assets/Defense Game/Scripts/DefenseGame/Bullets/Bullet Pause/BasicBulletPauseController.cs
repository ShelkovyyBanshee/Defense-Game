using UnityEngine;


namespace DefenseGame
{
    public class BasicBulletPauseController : PauseController
    {
        private IBulletMoveComponent _moveComponent;
        private Collider2D _collider;
        private AnimationController _animationController;

        protected override void ExecuteOnPause()
        {
            _animationController.AddStopAnimatorRequest();
            _moveComponent.AddStopRequest();
            _collider.enabled = false;
        }

        protected override void ExecuteOnContinue()
        {
            _animationController.RemoveStopAnimatorRequest();
            _moveComponent.RemoveStopRequest();
            _collider.enabled = true;
        }

        protected override void AwakeAdditional()
        {
            _moveComponent = GetComponent<IBulletMoveComponent>();
            _collider = GetComponent<Collider2D>();
            _animationController = GetComponent<AnimationController>();
        }
    }
}