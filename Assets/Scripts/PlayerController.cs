using UnityEngine;
using UnityEngine.InputSystem;
using Weapon;

public class PlayerController : SpaceShip
{
    
    [SerializeField] private LimitingRectangle limits;
    [SerializeField] private Weapons weapons;
    [SerializeField] private Range laserAngles;

    private LimitingRectangle _enemiesLimits;

    private enum WeaponType
    {
        None, Laser, Rocket, AutoCannon, GrandCannon
    }

    private WeaponType _weapon;

    protected override void Start()
    {
        base.Start();
        _enemiesLimits = FindObjectOfType<GameManager>().EnemiesLimits;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        var delta = context.ReadValue<Vector2>();
        if (limits.Check((Vector2) transform.position + delta))
            transform.Translate(delta);
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        var position = Camera.main!.ScreenToWorldPoint(context.ReadValue<Vector2>());
        if (!_enemiesLimits.Check(position)) return;
        var positionRounded = new Vector2(
            Mathf.Round(position.x), 
            Mathf.Round(position.y)
            );

        // laser
        var relativePosition = positionRounded - (Vector2) transform.position;
        var rotation = Mathf.Atan2(
            -relativePosition.x,
            relativePosition.y
        ) * Mathf.Rad2Deg;
        if (laserAngles.Check(rotation)) weapons.laser.transform.rotation = Quaternion.Euler(0, 0, rotation);    
                
        // rocket & auto cannon
        weapons.rocket.transform.position = positionRounded;
        weapons.autoCannon.transform.position = positionRounded;
        weapons.grandCannon.transform.position = positionRounded;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed || _weapon == WeaponType.None) return;
        weapons.ById((int) _weapon).Fire();
    }

    public void OnSelectWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        var key = context.ReadValue<float>();
        _weapon = (WeaponType) key;
        
        CheckActiveWeapon(weapons.laser, WeaponType.Laser);
        CheckActiveWeapon(weapons.rocket, WeaponType.Rocket);
        CheckActiveWeapon(weapons.autoCannon, WeaponType.AutoCannon);
        CheckActiveWeapon(weapons.grandCannon, WeaponType.GrandCannon);
    }

    private void CheckActiveWeapon(Component weapon, WeaponType weaponType)
    {
        weapon.gameObject.SetActive(_weapon == weaponType);
    }
}
