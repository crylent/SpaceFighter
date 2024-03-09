using System.Collections;
using Enemies;
using UnityEngine;

public class EnemyBot: MonoBehaviour
{
    private Enemy[] _ships;
    private GameManager _gameManager;

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
        foreach (var ship in _ships)
        {
            ship.MakeMove();
            yield return new WaitForSeconds(1f);
        }
        _gameManager.EnemyEndTurn();
    }
    
    private void ListShips()
    {
        _ships = FindObjectsOfType<Enemy>();
    }
}