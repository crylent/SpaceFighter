using System;
using UnityEngine;
using Weapon;

namespace Enemies
{
    public class Battleship: Enemy
    {
        /*
         * 
         */
        
        public override void MakeMove()
        {
            var deltaX = Player.transform.position.x - transform.position.x;
            if (Math.Abs(deltaX) < 0.01)
            {
                Attack(WeaponType.GrandCannon);
            }
            else
            {
                transform.Translate(-Mathf.Sign(deltaX), 0, 0);
            }
        }
    }
}