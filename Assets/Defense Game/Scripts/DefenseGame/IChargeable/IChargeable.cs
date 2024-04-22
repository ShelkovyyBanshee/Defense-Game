

namespace DefenseGame
{
    public interface IChargeable
    {
        public float RechargingProportion { get; }

        public bool IsFull { get; }

        public bool IsReadyToCharge { get; }

        public void ChargeByFull();
        public void ChargeWithOne();

        public void DiscountTimeUntilNextCharge(float time);
        public void StartNewRechargingCycle();
    }
}