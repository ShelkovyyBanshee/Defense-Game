using UnityEngine;


namespace DefenseGame
{
    public class BasicEnemyPauseController : PauseController
    {
        private IEnemyMoveComponent _moveComponent;
        private IColliderController _colliderController;
        private AnimationController _animationController;
        

        protected override void ExecuteOnContinue()
        {
            _animationController.RemoveStopAnimatorRequest();
            _moveComponent.RemoveStopRequest();
            _colliderController.RemoveTurnOffColliderRequest();
        }

        protected override void ExecuteOnPause()
        {
            _animationController.AddStopAnimatorRequest();
            _moveComponent.AddStopRequest();
            _colliderController.AddTurnOffColliderRequest();
        }

        protected override void AwakeAdditional()
        {
            _moveComponent = GetComponent<IEnemyMoveComponent>();
            _colliderController = GetComponent<IColliderController>();
            _animationController = GetComponent<AnimationController>();
        }
    }
}