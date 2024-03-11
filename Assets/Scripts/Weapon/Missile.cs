namespace Weapon
{
    public class Missile: WeaponWithProjectiles
    {
        public override void Fire()
        {
            StartCoroutine(LaunchProjectile(projectile, projectileStrikeTime));
            base.Fire();
        }
    }
}