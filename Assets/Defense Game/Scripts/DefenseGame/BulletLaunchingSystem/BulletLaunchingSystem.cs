using System.Collections.Generic;
using UnityEngine.WSA;


namespace DefenseGame
{
    public class BulletLaunchingSystem
    {
        public BulletLaunchingProvider Provider => _provider;
        public IBulletLauncher LastCaller => _lastCaller;

        private BulletLaunchingProvider _provider;
        private List<IBulletLaunchingModifier> _modifiers;

        private IBulletLauncher _lastCaller;
        
        public BulletLaunchingSystem()
        {
            _provider = new BulletLaunchingProvider(this);
            _modifiers = new List<IBulletLaunchingModifier>();
        }

        public void AddLaunchingModifier(IBulletLaunchingModifier modifier)
        {
            _modifiers.Add(modifier);
        }

        public BulletComponent LaunchBullet(IBulletLauncher _launcher,
            bool skipRecursiveModifiers=false)
        {
            var bullet = _launcher.SpawnBullet();

            _lastCaller = _launcher;

            foreach (var modifier in _modifiers)
            {
               if (!(modifier.IsRecursive && skipRecursiveModifiers))
                    modifier.ModifyLaunching(bullet, _launcher, _provider);
            }

            return bullet;
        }

        public BulletComponent RepeatLast(bool skipRecursiveModifiers=false)
        {
            var bullet = LaunchBullet(_lastCaller);

            foreach (var modifier in _modifiers)
            {
                if (!(modifier.IsRecursive && skipRecursiveModifiers))
                    modifier.ModifyLaunching(bullet, _lastCaller, _provider);
            }

            return bullet;
        }

        public void OnLevelClose()
        {
            _lastCaller = null;
            _modifiers = new List<IBulletLaunchingModifier>();
        }
    }
}