using UnityEngine;

public class GameManager: MonoBehaviour
{
    [SerializeField] private LimitingRectangle enemiesLimits;
    public LimitingRectangle EnemiesLimits => enemiesLimits;

    private PlayerController _player;
    private EnemyBot _bot;
    private bool _playerTurn = true;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _bot = FindObjectOfType<EnemyBot>();
    }

    public void PlayerEndTurn()
    {
        _playerTurn = false;
        _bot.StartTurn();
    }

    public void EnemyEndTurn()
    {
        _playerTurn = true;
        _player.RestoreActionPoints();
    }
}