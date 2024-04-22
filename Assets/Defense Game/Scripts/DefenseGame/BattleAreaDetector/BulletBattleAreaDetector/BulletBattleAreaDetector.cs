using UnityEngine;


namespace DefenseGame
{
    public class BulletBattleAreaDetector : BattleAreaDetector
    {
        [SerializeField] private bool _baseIsOnValue;
        private bool _isOn;

        public void SwitchOn()
        {
            _isOn = true;
        }

        public void SwitchOff()
        {
            _isOn = false;
        }

        protected override void OnEnterBattleArea()
        {
            if (_isOn)
                base.OnEnterBattleArea();
        }

        protected override void OnExitBattleArea()
        {
            if (_isOn)
                base.OnExitBattleArea();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _isOn = _baseIsOnValue;
        }
    }
}