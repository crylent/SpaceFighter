using JetBrains.Annotations;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Weapon;

public abstract class SpaceShip: MonoBehaviour
{
    [SerializeField] private string shipName;
    [SerializeField] protected Weapons weapons;
    [SerializeField] private int maxDurability = 100;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] private int actionPoints = 1;
    [SerializeField] [CanBeNull] protected ActionPoints actionPointsUI;
    [SerializeField] protected UnityEvent<int> onActionPointsChanged;

    public string Name => shipName;
    
    public int MaxActionPoints => actionPoints;
    protected int ActionPoints;

    public int MaxDurability => maxDurability;
    public int Durability { get; private set; }

    protected virtual void Start()
    {
        Durability = MaxDurability;
        ActionPoints = actionPoints;
        Instantiate(healthBar, GameObject.FindWithTag("HealthBar Layer").transform).AttachTo(this);
        
        if (actionPointsUI.IsUnityNull()) return;
        var pointsUI = Instantiate(actionPointsUI, FindObjectOfType<Canvas>().transform);
        pointsUI.AttachTo(this);
        onActionPointsChanged.AddListener(points => pointsUI.OnPointsChanged(points));
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;
        Durability -= damage;
        if (Durability <= 0) Destruction();
    }

    protected abstract void Destruction();

    public void RestoreActionPoints()
    {
        ActionPoints = MaxActionPoints;
        onActionPointsChanged.Invoke(ActionPoints);
    }
}