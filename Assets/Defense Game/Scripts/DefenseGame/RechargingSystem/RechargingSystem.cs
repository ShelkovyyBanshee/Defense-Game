using System.Collections.Generic;
using UnityEngine;

namespace DefenseGame
{
    public class RechargingSystem : MonoBehaviour
    {
        private List<IChargeable> _chargeable;
        private bool _isOn;

        public void Initialize()
        {
            _chargeable = new List<IChargeable>();
            _isOn = true;
        }

        public void ChargeAllByFull()
        {
            foreach (var el in _chargeable)
                el.ChargeByFull();
        }

        public void AddChargeable(IChargeable obj)
        {
            _chargeable.Add(obj);
        }

        public void ClearChargeableObjects()
        {
            _chargeable = new List<IChargeable>();
        }

        private void Update()
        {
            if (!_isOn)
                return;

            foreach (var el in _chargeable)
            {
                if (!el.IsFull)
                    el.DiscountTimeUntilNextCharge(Time.deltaTime);

                if (el.IsReadyToCharge)
                {
                    el.ChargeWithOne();
                    el.StartNewRechargingCycle();
                }
            }
        }
    }
}