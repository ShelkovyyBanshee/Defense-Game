using UnityEngine;
using System;
using Unity.VisualScripting;
using UsefulObjects;

namespace DefenseGame
{
    public class BasicGun : Weapon, IPoolingSystemUser, IBulletLauncher, IBulletLaunchingUser
    {
        public event Action onEquip;
        public event Action onAnimatedEquip;
        public event Action onShoot;

        public override bool IsLocked => _isLocked.Flag;
        public BulletComponent BulletPrefab => _bulletPrefab;

        protected BulletLaunchingProvider BulletLaunchingProvider => _bulletLaunchingProvider;
        protected Transform Transform => _transform;
        protected Transform BulletSpawnPoint => _bulletSpawnPoint;

        [SerializeField] private BulletComponent _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPoint;

        private Transform _transform;
        private PoolingSystem _poolingSystem;
        private BulletLaunchingProvider _bulletLaunchingProvider;
        private TrueFlagService _isLocked;

        public void SetPoolingSystem(PoolingSystem poolingSystem)
        {
            _poolingSystem = poolingSystem;
        }

        public void SetBulletLaunchingProvider(BulletLaunchingProvider provider)
        {
            _bulletLaunchingProvider = provider;
        }

        public override void OnEquip(bool isAnimated)
        {
            onEquip?.Invoke();
            if (isAnimated)
                onAnimatedEquip?.Invoke();
        }

        public override void TryAttack()
        {
            if (!_isLocked.Flag)
            {
                AddLockRequest();
                onShoot?.Invoke();
            }
        }

        public void AddLockRequest()
        {
            _isLocked.AddTrueRequest();
        }

        public void RemoveLockRequest()
        {
            _isLocked.RemoveTrueRequest();
        }

        public BulletComponent SpawnBullet()
        {
            var bullet = _poolingSystem.Get(_bulletPrefab);

            bullet.Position = _bulletSpawnPoint.position;
            var bulletSorting = bullet.GetComponent<BulletSortingController>();
            bulletSorting.SetSortPosition(new Vector2(_transform.position.x, 
                transform.position.y + (bulletSorting.ShowOverWeapon ? -0.1f : 0.1f)));

            bullet.StartLiveCycle();
            return bullet;
        }

        protected virtual void LaunchBullet()
        {
            _bulletLaunchingProvider.LaunchBullet(this);
        }

        private void OnAttackAnimationEnds() 
        {
            RemoveLockRequest();
            EndAttack();
        }

        protected virtual void Awake()
        {
            _transform = transform;
            _isLocked = new TrueFlagService();
        }

        private void OnDisable()
        {
            _isLocked.DeleteRequestInfo();
        }
    }
}