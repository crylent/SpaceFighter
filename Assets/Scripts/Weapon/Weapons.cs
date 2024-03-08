using System;

namespace Weapon
{
    [Serializable]
    public class Weapons
    {
        public Weapon laser;
        public Weapon rocket;
        public Weapon autoCannon;

        public Weapon ById(int id) => 
            id switch 
            {
                1 => laser,
                2 => rocket,
                3 => autoCannon,
                _ => null 
            };
    }
}