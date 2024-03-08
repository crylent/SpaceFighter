using System.Collections;
using UnityEngine;

namespace Weapon
{
    public class AutoCannon: WeaponWithProjectiles
    {
        [SerializeField] private int projectilesNumber = 5;
        [SerializeField] private float interval = 0.1f;

        public override void Fire()
        {
            StartCoroutine(LaunchProjectiles());
            base.Fire();
        }

        private IEnumerator LaunchProjectiles()
        {
            for (var i = 0; i < projectilesNumber; i++)
            {
                StartCoroutine(LaunchProjectile(projectile, projectileStrikeTime));
                yield return new WaitForSeconds(interval);
            }
        }
    }
}