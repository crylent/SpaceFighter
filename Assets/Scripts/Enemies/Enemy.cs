using System;
using System.Collections;
using UnityEngine;
using Weapon;

namespace Enemies
{
    public abstract class Enemy : SpaceShip
    {
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material outlineMaterial;

        private const float DelayBeforeFire = 0.25f;

        private Renderer _renderer;
        protected PlayerController Player;
        private EnemyBot _bot;

        private bool _isMakingMove;
        public bool IsMakingMove() => _isMakingMove;

        protected override void Start()
        {
            base.Start();
            _renderer = GetComponent<Renderer>();
            Player = FindObjectOfType<PlayerController>();
            _bot = FindObjectOfType<EnemyBot>();
        }

        protected override void Destruction()
        {
            _bot.DelistShip(this);
            Destroy(gameObject);
        }

        public void SetThreatened(bool isThreatened = true)
        {
            _renderer.material = isThreatened ? outlineMaterial : defaultMaterial;
        }

        public void MakeMoves()
        {
            StartCoroutine(MakeMovesCoroutine());
        }

        private IEnumerator MakeMovesCoroutine()
        {
            _isMakingMove = true;
            while (ActionPoints > 0)
            {
                MakeMove();
                ActionPoints -= 1;
                onActionPointsChanged.Invoke(ActionPoints);
                yield return new WaitForSeconds(1f);
            }
            _isMakingMove = false;
        }

        protected abstract void MakeMove();

        protected bool MoveToPlayerOnX()
        {
            var deltaX = PlayerXDiff();
            if (deltaX == 0) return false;
            var delta = -Mathf.Sign(deltaX);
            var pos = transform.position;
            pos.x += delta;
            if (_bot.OccupiedPositions.Contains(Utility.RoundVectorToInt(pos)))
                return false;
            transform.Translate(delta, 0, 0);
            return true;
        }

        protected void Attack(WeaponType weapon)
        {
            switch (weapon)
            {
                case WeaponType.None:
                    break;
                case WeaponType.Laser:
                    break;
                case WeaponType.Rocket:
                    break;
                case WeaponType.AutoCannon:
                    AutoCannonFire();
                    break;
                case WeaponType.GrandCannon:
                    GrandCannonFire();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(weapon), weapon, null);
            }
        }

        private void GrandCannonFire()
        {
            StartCoroutine(CannonFire(weapons.grandCannon));
        }

        private void AutoCannonFire()
        {
            StartCoroutine(CannonFire(weapons.autoCannon));
        }

        private IEnumerator CannonFire(Weapon.Weapon weapon)
        {
            weapon.transform.position = Player.transform.position;
            yield return new WaitForSeconds(DelayBeforeFire);
            weapon.Fire();
        }

        protected int PlayerXDiff() => Utility.DeltaX(Player.transform, transform);
    }
}
