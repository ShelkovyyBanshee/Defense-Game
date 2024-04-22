

namespace DefenseGame
{
    public interface IBulletLauncher
    {
        public BulletComponent BulletPrefab { get; }

        public BulletComponent SpawnBullet();
    }
}