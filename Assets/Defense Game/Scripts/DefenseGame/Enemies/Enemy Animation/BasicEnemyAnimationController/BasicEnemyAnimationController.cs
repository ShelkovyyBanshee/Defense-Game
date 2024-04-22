using UnityEngine;
using UsefulObjects;


namespace DefenseGame
{
    public class BasicEnemyAnimationController : AnimationController, IActivationAffected
    {
        public InitOrder InitAfterActivationOrder => InitOrder.AnimationControllerOrder;

        [SerializeField] private string _deathTriggerName;
        [SerializeField] private string _disappearTriggerName;
        [SerializeField] private string _hitTriggerName;

        private EnemyComponent _enemyComponent;
        private IEnemyHealthComponent _enemyHealth;
        private IEnemyDeathController _enemyHealthObserver;
        private IEnemyMoveComponent _enemyMoveComponent;

        public virtual void InitializeAfterActivation()
        {
            SynchronizeWithMoveComponent();
        }

        public override void RemoveStopAnimatorRequest()
        {
            base.RemoveStopAnimatorRequest();

            if (IsOnStop)
                SynchronizeWithMoveComponent(); 
        }

        private void SynchronizeWithMoveComponent()
        {
            Animator.speed = _enemyMoveComponent.SpeedProportion;
        }

        private void ActivateDisappearAnimation()
        {
            Animator.SetTrigger(_disappearTriggerName);
        }

        private void OnEnemyDefeated()
        {
            Animator.SetTrigger(_deathTriggerName);
            _enemyMoveComponent.AddStopRequest();
        }

        private void OnHit(DamageType damageType, float damage)
        {
            Animator.SetTrigger(_hitTriggerName);
        }

        private void OnDisappearAnimationIsOver()
        {
            _enemyComponent.Disappear();
        }

        protected override void Awake()
        {
            base.Awake();
            _enemyMoveComponent = GetComponent<IEnemyMoveComponent>();
            _enemyComponent = GetComponent<EnemyComponent>();
            _enemyHealth = GetComponent<IEnemyHealthComponent>();
            _enemyHealthObserver = GetComponent<IEnemyDeathController>();
        }

        private void OnEnable()
        {
            _enemyHealthObserver.onWasDefeated += OnEnemyDefeated;
            _enemyHealth.onHit += OnHit;
        }

        private void OnDisable()
        {
            _enemyHealthObserver.onWasDefeated -= OnEnemyDefeated;
            _enemyHealth.onHit -= OnHit;
        }
    }
}