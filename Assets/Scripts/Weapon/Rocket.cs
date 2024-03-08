namespace Weapon
{
    public class Rocket: WeaponWithProjectiles
    {
        public override void Fire()
        {
            StartCoroutine(LaunchProjectile(projectile, projectileStrikeTime));
            base.Fire();
        }
    }
}