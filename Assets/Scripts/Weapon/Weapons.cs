using System;

namespace Weapon
{
    [Serializable]
    public class Weapons
    {
        public Laser laser;
        public Missile missile;
        public AutoCannon autoCannon;
        public GrandCannon grandCannon;

        public Weapon ById(int id) => 
            id switch 
            {
                1 => laser,
                2 => missile,
                3 => autoCannon,
                4 => grandCannon,
                _ => null 
            };
    }
}