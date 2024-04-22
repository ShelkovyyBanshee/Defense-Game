using UnityEngine;

namespace DefenseGame
{
    public class BattleFieldSystemInjector : IFabricModifier
    {
        private BattleFieldSystem _battleFieldSystem;

        public BattleFieldSystemInjector(BattleFieldSystem battleFieldSystem)
        {
            _battleFieldSystem = battleFieldSystem;
        }

        public void ModifyObject(MonoBehaviour obj)
        {
            var componentsToInject = obj.GetComponents<IBattleFieldSystemUser>();

            if (componentsToInject != null)
                foreach (var component in componentsToInject)
                    component.SetBattleFieldSystem(_battleFieldSystem);
        }
    }
}