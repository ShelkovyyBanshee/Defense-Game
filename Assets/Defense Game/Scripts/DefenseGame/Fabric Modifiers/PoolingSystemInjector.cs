using UnityEngine;
using Pausing;


namespace DefenseGame
{
    public class PoolingSystemInjector : IFabricModifier
    {
        private PoolingSystem _poolingSystem;

        public PoolingSystemInjector(PoolingSystem poolingSystem)
        {
            _poolingSystem = poolingSystem;
        }

        public void ModifyObject(MonoBehaviour obj)
        {
            var poolingUser = obj.GetComponent<IPoolingSystemUser>();

            if (poolingUser != null)
                poolingUser.SetPoolingSystem(_poolingSystem);
        }
    }
}