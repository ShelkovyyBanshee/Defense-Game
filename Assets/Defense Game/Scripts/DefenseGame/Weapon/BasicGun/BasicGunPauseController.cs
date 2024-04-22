using UnityEngine;


namespace DefenseGame
{
    public class BasicGunPauseController : PauseController
    {
        private AnimationController _animationController;
        private BasicGun _gun;

        protected override void AwakeAdditional()
        {
            _animationController = GetComponent<AnimationController>();
            _gun = GetComponent<BasicGun>();
        }

        protected override void ExecuteOnContinue()
        {
            _animationController.RemoveStopAnimatorRequest();
            _gun.RemoveLockRequest();
        }

        protected override void ExecuteOnPause()
        {
            _animationController.AddStopAnimatorRequest();
            _gun.AddLockRequest();
        }

        private void OnEnable()
        {
            InitializeAfterActivation();
        }
    }
}