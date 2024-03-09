using UI;
using UnityEngine;
using Weapon;

public class SpaceShip: MonoBehaviour
{
    [SerializeField] protected Weapons weapons;
    [SerializeField] private int maxDurability = 100;
    [SerializeField] protected HealthBar healthBar;

    public int MaxDurability => maxDurability;
    public int Durability { get; private set; }

    protected virtual void Start()
    {
        Durability = MaxDurability;
        Instantiate(healthBar, FindObjectOfType<Canvas>().transform).AttachTo(this);
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;
        Durability -= damage;
    }
}