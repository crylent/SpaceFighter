using System.Linq;
using UnityEngine;

namespace UI
{
    public class SpaceshipHoverTooltip: HoverTooltipTarget
    {
        [SerializeField] private SpaceShip ship;
        [SerializeField] private Weapon.Weapon[] weapons;

        public override string MakeDesc() =>
            weapons.Aggregate(
                "Durability: " + ship.Durability + "/" + ship.MaxDurability + "\n\n",
                (str, weapon) => str + weapon.GetWeaponName() + " (" + weapon.MaxDamage + " DMG)\n"
            );
    }
}