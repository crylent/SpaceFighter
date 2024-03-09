using System;
using System.Collections;
using UnityEngine;
using Weapon;

namespace Enemies
{
    public class Enemy : SpaceShip
    {
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material outlineMaterial;

        private const float DelayBeforeFire = 0.25f;

        private Renderer _renderer;
        protected PlayerController Player;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _renderer = GetComponent<Renderer>();
            Player = FindObjectOfType<PlayerController>();
        }

        protected override void Destruction()
        {
            Destroy(gameObject);
        }

        public void SetThreatened(bool isThreatened = true)
        {
            _renderer.material = isThreatened ? outlineMaterial : defaultMaterial;
        }
        
        public virtual void MakeMove() {}

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
                    break;
                case WeaponType.GrandCannon:
                    StartCoroutine(GrandCannonFire());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(weapon), weapon, null);
            }
        }

        private IEnumerator GrandCannonFire()
        {
            weapons.grandCannon.transform.position = Player.transform.position;
            yield return new WaitForSeconds(DelayBeforeFire);
            weapons.grandCannon.Fire();
        }
    }
}
