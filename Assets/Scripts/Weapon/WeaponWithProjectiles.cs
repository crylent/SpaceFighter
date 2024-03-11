using UnityEngine;

namespace Weapon
{
    public abstract class WeaponWithProjectiles: Weapon
    {
        [SerializeField] protected GameObject projectile;
        [SerializeField] protected float projectileStrikeTime = 0.083f;
    }
}