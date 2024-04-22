using UnityEngine;

namespace DefenseGame
{
    public interface IBulletMoveComponent
    {
        public float DistanceDelta { get; }
        public bool IsMoving { get; }

        public void AddStopRequest();

        public void RemoveStopRequest();
    }
} 