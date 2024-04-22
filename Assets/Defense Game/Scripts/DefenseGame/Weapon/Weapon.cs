using UnityEngine;
using System;

namespace DefenseGame
{
    public abstract class Weapon : MonoBehaviour
    {
        public event Action onAttackEnds;

        public abstract bool IsLocked { get; }

        public abstract void TryAttack();
        public abstract void OnEquip(bool isAnimated);

        protected void EndAttack()
        {
            onAttackEnds?.Invoke();
        }
    }
}