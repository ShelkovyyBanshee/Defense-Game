using UnityEngine;

namespace DefenseGame
{
    public class FullCharger : IFabricModifier
    {
        public void ModifyObject(MonoBehaviour obj)
        {
            var chargeableComponents = obj.GetComponents<IChargeable>();

            foreach (var component in chargeableComponents)
            {
                component.ChargeByFull();
            }
        }
    }
}