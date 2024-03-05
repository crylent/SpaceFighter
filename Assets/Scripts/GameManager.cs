using UnityEngine;

public class GameManager: MonoBehaviour
{
    [SerializeField] private LimitingRectangle enemiesLimits;
    public LimitingRectangle EnemiesLimits => enemiesLimits;
}