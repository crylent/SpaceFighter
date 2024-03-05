using System;

[Serializable]
public class Weapons
{
    public Weapon laser;
    public Weapon rocket;

    public Weapon ById(int id) => 
        id switch 
        {
            1 => laser,
            2 => rocket,
            _ => null 
        };
}