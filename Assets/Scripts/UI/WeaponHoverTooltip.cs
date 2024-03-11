using UnityEngine;

namespace UI
{
    public class WeaponHoverTooltip: HoverTooltipTarget
    {
        [SerializeField] private Weapon.Weapon weapon;

        public override string GetTitle() => weapon.GetWeaponName();

        public override string MakeDesc()
        {
            if (weapon.DamageReductionFromDistance == 0)
            {
                return "DMG: " + weapon.MaxDamage;
            }
            return "DMG Base: " + weapon.MaxDamage + "\nDistance Penalty: " + weapon.DamageReductionFromDistance;
        }
    }
}