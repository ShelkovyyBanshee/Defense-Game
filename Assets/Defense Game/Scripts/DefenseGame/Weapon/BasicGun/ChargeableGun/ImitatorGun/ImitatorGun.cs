

namespace DefenseGame
{
    public class ImitatorGun : ChargeableGun
    {
        protected override void LaunchBullet()
        {
            if (BulletLaunchingProvider.LastCaller == null)
            {
                base.LaunchBullet();
            }
            else
            {
                var bullet = BulletLaunchingProvider.RepeatLast();
                bullet.Position = BulletSpawnPoint.position;

                var bulletSorting = bullet.GetComponent<BulletSortingController>();
                bulletSorting.SetSortPosition(Transform.position);
            }
            
        }
    }
}