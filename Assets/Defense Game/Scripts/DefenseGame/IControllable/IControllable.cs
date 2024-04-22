

namespace DefenseGame
{
    public interface IControllable
    {
        public Weapon SwitchWeapon(Weapon newWeapon);
        public void MoveUpDown(float moveYDirection);
        public void Attack();
    }
}