namespace Weapon
{
    public class Missile: WeaponWithProjectiles
    {
        public override void Fire()
        {
            StartCoroutine(LaunchProjectile(projectile, projectileStrikeTime));
            base.Fire();
        }

        public override string GetWeaponName() => "Missile";
    }
}