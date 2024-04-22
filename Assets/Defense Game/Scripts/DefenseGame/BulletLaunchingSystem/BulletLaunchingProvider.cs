

namespace DefenseGame
{
    public class BulletLaunchingProvider
    {
        public IBulletLauncher LastCaller => _system.LastCaller;
        private BulletLaunchingSystem _system;

        public BulletLaunchingProvider(BulletLaunchingSystem system)
        {
            _system = system;
        }

        public void LaunchBullet(IBulletLauncher launcher, bool skipRecursiveModifiers=false)
        {
            _system.LaunchBullet(launcher, skipRecursiveModifiers);
        }

        public BulletComponent RepeatLast(bool skipRecursiveModifiers=false)
        {
            return _system.RepeatLast(skipRecursiveModifiers);
        }
    }
}