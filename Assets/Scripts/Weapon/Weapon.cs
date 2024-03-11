using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Weapon
{
    [RequireComponent(
        typeof(Collider2D), 
        typeof(Rigidbody2D),
        typeof(Animator)
    )]
    public abstract class Weapon: MonoBehaviour
    {
        [SerializeField] private int damage = 10;
        public int MaxDamage => damage;
        
        [SerializeField] private int damageReductionFromDistance;
        public int DamageReductionFromDistance => damageReductionFromDistance;

        private Animator _animator;
        private static readonly int OnAttackTrigger = Animator.StringToHash("onAttack");

        private readonly List<SpaceShip> _shipsThreatened = new();

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            SetShipThreatened(col, true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            SetShipThreatened(other, false);
        }

        private void SetShipThreatened(Component other, bool threatened)
        {
            var isEnemy = other.CompareTag("Enemy");
            if (!isEnemy && !other.CompareTag("Player")) return;
            var ship = other.GetComponent<SpaceShip>();
            if (isEnemy)
            {
                (ship as Enemy)!.SetThreatened(threatened);
            }
            if (threatened) _shipsThreatened.Add(ship);
            else _shipsThreatened.Remove(ship);
        }

        public virtual void Fire()
        {
            _animator.SetTrigger(OnAttackTrigger);
            var ships = new List<SpaceShip>(_shipsThreatened);
            foreach (var ship in ships)
            {
                ship.TakeDamage(CalculateDamage(ship));
            }
        }

        public int CalculateDamage(SpaceShip target)
        {
            var distance = Utility.OrthoDistance(target.transform, transform.parent);
            var resDamage = damage - damageReductionFromDistance * Mathf.RoundToInt(distance);
            return resDamage > 0 ? resDamage : 0;
        }

        private bool _hasActiveProjectile;

        protected IEnumerator LaunchProjectile(GameObject projectile, float projectileStrikeTime)
        {
            var tr = transform;
            var targetPosition = tr.position;
            var currentPosition = tr.parent.position;
            var deltaPosition = targetPosition - currentPosition;
            var instance = Instantiate(
                projectile,
                currentPosition,
                Quaternion.FromToRotation(Vector3.up, deltaPosition)
            );
            _hasActiveProjectile = true;
            var projectileRigidbody = instance.GetComponent<Rigidbody2D>();
            projectileRigidbody.velocity = deltaPosition / projectileStrikeTime;
            yield return new WaitForSeconds(projectileStrikeTime);
            Destroy(instance);
            _hasActiveProjectile = false;
        }

        public bool CanBeDeactivated() => !_hasActiveProjectile;

        public abstract string GetWeaponName();
    }
}