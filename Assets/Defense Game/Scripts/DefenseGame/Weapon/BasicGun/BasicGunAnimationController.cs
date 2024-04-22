using UnityEngine;


namespace DefenseGame
{
    public class BasicGunAnimationController : AnimationController
    {
        [SerializeField] private string _equipTriggerName;
        [SerializeField] private string _shootTriggerName;

        private BasicGun _gun;

        private void OnShoot()
        {
            Animator.SetTrigger(_shootTriggerName);
        }

        private void OnAnimatedEquip()
        {
            Animator.SetTrigger(_equipTriggerName);
        }

        protected override void Awake()
        {
            base.Awake();
            _gun = GetComponent<BasicGun>();
        }

        private void OnEnable()
        {
            _gun.onShoot += OnShoot;
            _gun.onAnimatedEquip += OnAnimatedEquip;
        }

        private void OnDisable()
        {
            _gun.onShoot -= OnShoot;
            _gun.onAnimatedEquip -= OnAnimatedEquip;
        }
    }
}