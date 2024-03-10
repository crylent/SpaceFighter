using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using UnityEngine;

public class EnemyBot: MonoBehaviour
{
    private List<Enemy> _ships;
    private GameManager _gameManager;
    public List<(int, int)> OccupiedPositions => _ships
        .Select(ship => Utility.RoundVectorToInt(ship.transform.position))
        .ToList();

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        ListShips();
    }

    public void StartTurn()
    {
        StartCoroutine(DoTurn());
    }

    private IEnumerator DoTurn()
    {
        var ships = new List<Enemy>(_ships);
        foreach (var ship in ships)
        {
            ship.MakeMoves();
            yield return new WaitWhile(ship.IsMakingMove);
        }
        _gameManager.EnemyEndTurn();
    }
    
    private void ListShips()
    {
        _ships = FindObjectsOfType<Enemy>().ToList();
    }

    public void DelistShip(Enemy ship)
    {
        _ships.Remove(ship);
    }
}