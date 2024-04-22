


namespace DefenseGame
{
    public interface IBulletLaunchingModifier
    {
        public bool IsRecursive { get; }

        public void ModifyLaunching(BulletComponent bullet,
            IBulletLauncher launcher, BulletLaunchingProvider provider);
    }
}