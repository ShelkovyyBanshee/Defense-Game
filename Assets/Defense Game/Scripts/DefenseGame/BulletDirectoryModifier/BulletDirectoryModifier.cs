using UnityEngine;

namespace DefenseGame
{
    public class BulletDirectoryModifier : IBulletLaunchingModifier
    {
        public bool IsRecursive => false;

        private Transform _directory;

        public BulletDirectoryModifier(Transform directory)
        {
            _directory = directory;
        }

        public void ModifyLaunching(BulletComponent bullet, IBulletLauncher launcher,
            BulletLaunchingProvider provider)
        {
            bullet.transform.SetParent(_directory);
        }
    }
}