using System;
using Weapon;

namespace Enemies
{
    public class Destroyer: Enemy
    {
        protected override void MakeMove(int actionsRemain)
        {
            var deltaX = PlayerXDiff();
            if (deltaX == 0)
            {
                var autoCannonDamage = weapons.autoCannon.CalculateDamage(Player);
                var grandCannonDamage = weapons.grandCannon.CalculateDamage(Player);
                Attack(grandCannonDamage > autoCannonDamage ? WeaponType.GrandCannon : WeaponType.AutoCannon);
                return;
            }
            
            var pos = transform.position;
            var damage1 = weapons.autoCannon.CalculateDamage(Player) * actionsRemain;
            MoveToPlayerOnX();
            if (damage1 == 0) return; // if can't deal damage now, stay in the new position
            var damage2 = weapons.autoCannon.CalculateDamage(Player) * (actionsRemain - 1);
            if (damage2 > damage1) return; // if can increase damage, stay in the new position
            if (Math.Abs(deltaX) == 1)
            {
                var damage3 = weapons.grandCannon.CalculateDamage(Player) * (actionsRemain - 1);
                if (damage3 > damage1) return; // if can increase damage, stay in the new position
            }
            transform.position = pos; // if can't increase damage, return to the previous position and fire
            Attack(WeaponType.AutoCannon);
        }
    }
}