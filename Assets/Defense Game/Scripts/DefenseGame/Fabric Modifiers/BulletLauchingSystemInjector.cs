using UnityEngine;

namespace DefenseGame
{
    public class BulletLaunchingSystemInjector : IFabricModifier
    {
        private BulletLaunchingProvider _provider;

        public BulletLaunchingSystemInjector(BulletLaunchingProvider provider)
        {
            _provider = provider;
        }

        public void ModifyObject(MonoBehaviour obj)
        {
            var launchingUser = obj.GetComponent<IBulletLaunchingUser>();

            if (launchingUser != null)
                launchingUser.SetBulletLaunchingProvider(_provider);
        }
    }
}