using UnityEngine;


namespace DefenseGame
{
    public class BulletDeathDistanceComponent : MonoBehaviour, IActivationAffected,
        IBattleFieldSystemUser
    {
        public InitOrder InitAfterActivationOrder => _initOnActivationOrder;

        [SerializeField] private InitOrder _initOnActivationOrder;

        private float _deathDistance;
        private float _distancePassed;
        private IBulletMoveComponent _moveComponent;
        private BulletComponent _bullet;

        public void InitializeAfterActivation()
        {
            _distancePassed = 0;
        }

        public void SetBattleFieldSystem(BattleFieldSystem battleFieldSystem)
        {
            _deathDistance = battleFieldSystem.BattleArea.width;
        }

        private void FixedUpdate()
        {
            if (_moveComponent.IsMoving) 
                _distancePassed += _moveComponent.DistanceDelta;

            if (_distancePassed >= _deathDistance)
            {
                _bullet.Die();
            }
        }

        private void Awake()
        {
            _moveComponent = GetComponent<IBulletMoveComponent>();
            _bullet = GetComponent<BulletComponent>();
        }
    }
}