using System;

namespace DefenseGame
{
    public interface ICharacterInput
    {
        public event Action onInputUpdate;

        public bool IsMovingUpInFrame { get; }
        public bool IsMovingDownInFrame { get; }

        public bool IsSwitchingToPreviousInFrame { get; }
        public bool IsSwitchingToNextInFrame { get; }
        public bool IsSwitchingByIndexInFrame { get; }
        public int ChosenWeaponIndex { get; }

        public bool IsAttackingInFrame { get; }
        public bool IsAutomaticAttackModeOn { get; }
    }
}