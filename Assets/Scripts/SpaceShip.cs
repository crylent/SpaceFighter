using UnityEngine;

public class SpaceShip: MonoBehaviour
{
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
        Durability -= damage;
    }
}