namespace Weapon
{
    public class GrandCannon: WeaponWithProjectiles
    {
        public override void Fire()
        {
            StartCoroutine(LaunchProjectile(projectile, projectileStrikeTime));
            base.Fire();
        }

        public override string GetWeaponName() => "Grand Cannon";
    }
}