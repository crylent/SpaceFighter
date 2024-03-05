using System.Collections.Generic;
using UnityEngine;

[RequireComponent(
    typeof(Collider2D), 
    typeof(Rigidbody2D),
    typeof(Animator)
    )]
public class Weapon: MonoBehaviour
{
    private Animator _animator;
    private static readonly int OnAttackTrigger = Animator.StringToHash("onAttack");

    private readonly List<Enemy> _enemiesThreatened = new();

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        SetEnemyThreatened(col, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        SetEnemyThreatened(other, false);
    }

    private void SetEnemyThreatened(Component other, bool threatened)
    {
        if (!other.CompareTag("Enemy")) return;
        
        var enemy = other.GetComponent<Enemy>();
        enemy.SetThreatened(threatened);
        if (threatened) _enemiesThreatened.Add(enemy);
        else _enemiesThreatened.Remove(enemy);
    }

    public void Fire()
    {
        _animator.SetTrigger(OnAttackTrigger);
        foreach (var enemy in _enemiesThreatened)
        {
            enemy.TakeDamage(10);
        }
    }
}