using Weapon;

namespace Enemies
{
    public class Battleship: Enemy
    {
        protected override void MakeMove(int actionsRemain)
        {
            if (PlayerXDiff() == 0) Attack(WeaponType.GrandCannon);
            else MoveToPlayerOnX();
        }
    }
}