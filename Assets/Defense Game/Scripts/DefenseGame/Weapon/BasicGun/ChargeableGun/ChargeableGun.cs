using Unity.IO.LowLevel.Unsafe;
using UnityEngine;


namespace DefenseGame
{
    public class ChargeableGun : BasicGun, IChargeable
    {
        public float RechargingProportion => 
            (_rechargingTime - _timeUntilNextRecharge) / _rechargingTime;

        public bool IsFull => _bulletsInMagazine == _magazineSize;
        public bool IsReadyToCharge => _timeUntilNextRecharge == 0;

        [Header("Recharging")]
        [SerializeField] private int _magazineSize;
        [SerializeField] private float _rechargingTime;
        [SerializeField] private bool _isInfinite;

        [SerializeField] private int _bulletsInMagazine;
        private float _timeUntilNextRecharge;

        public override void TryAttack()
        {
            if (_bulletsInMagazine > 0 || _isInfinite)
            {
                _bulletsInMagazine -= 1;
                base.TryAttack();
            }
        }

        public virtual void ChargeByFull()
        {
            _bulletsInMagazine = _magazineSize;
            _timeUntilNextRecharge = _rechargingTime;
        }

        public void ChargeWithOne()
        {
            _bulletsInMagazine += _bulletsInMagazine < _magazineSize ? 1 : 0;
        }

        public void DiscountTimeUntilNextCharge(float time)
        {
            _timeUntilNextRecharge = _timeUntilNextRecharge > time ? _timeUntilNextRecharge - time : 0;
        }

        public void StartNewRechargingCycle()
        {
            _timeUntilNextRecharge = _rechargingTime;
        }
    }
}