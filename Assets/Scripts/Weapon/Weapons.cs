using System;

namespace Weapon
{
    [Serializable]
    public class Weapons
    {
        public Weapon laser;
        public Weapon missile;
        public Weapon autoCannon;
        public Weapon grandCannon;

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