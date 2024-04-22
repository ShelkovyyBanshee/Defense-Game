using System;


namespace DefenseGame
{
    public interface IEnemyMoveComponent
    {
        public abstract float SpeedProportion { get; }

        public abstract void AddStopRequest();
        public abstract void RemoveStopRequest();
    }
}