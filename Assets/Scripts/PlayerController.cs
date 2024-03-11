using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Weapon;

public class PlayerController : SpaceShip
{
    
    [SerializeField] private LimitingRectangle limits;
    [SerializeField] private Range laserAngles;
    
    private GameManager _gameManager;

    private WeaponType _weapon;

    protected override void Start()
    {
        base.Start();
        _gameManager = FindObjectOfType<GameManager>();
    }

    protected override void Destruction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!context.performed || ActionPoints <= 0) return;

        var delta = context.ReadValue<Vector2>();
        if (!limits.Check((Vector2)transform.position + delta)) return;
        transform.Translate(delta);
        WasteActionPoints();
        SetGrandCannonX();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        var position = Camera.main!.ScreenToWorldPoint(context.ReadValue<Vector2>());
        if (!_gameManager.EnemiesLimits.Check(position)) return;
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
        weapons.missile.transform.position = positionRounded;
        weapons.autoCannon.transform.position = positionRounded;
        
        // grand cannon (can fire forward only)
        weapons.grandCannon.transform.position = positionRounded;
        SetGrandCannonX();
    }

    private void SetGrandCannonX()
    {
        Utility.SetX(weapons.grandCannon.transform, transform.position.x);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed || _weapon == WeaponType.None || ActionPoints <= 0) return;
        weapons.ById((int) _weapon).Fire();
        WasteActionPoints();
    }

    public void OnSelectWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        var key = context.ReadValue<float>();
        SelectWeapon((WeaponType) key);
    }

    private void SelectWeapon(WeaponType weapon)
    {
        if (ActionPoints <= 0 && weapon != WeaponType.None) return;
        _weapon = weapon;
        
        CheckActiveWeapon(weapons.laser, WeaponType.Laser);
        CheckActiveWeapon(weapons.missile, WeaponType.Missile);
        CheckActiveWeapon(weapons.autoCannon, WeaponType.AutoCannon);
        CheckActiveWeapon(weapons.grandCannon, WeaponType.GrandCannon);
    }

    private void CheckActiveWeapon(Weapon.Weapon weapon, WeaponType weaponType)
    {
        if (_weapon == weaponType) weapon.gameObject.SetActive(true);
        else StartCoroutine(DeactivateWeapon(weapon));
    }

    private static IEnumerator DeactivateWeapon(Weapon.Weapon weapon)
    {
        yield return new WaitUntil(weapon.CanBeDeactivated);
        weapon.gameObject.SetActive(false);
    }

    private void WasteActionPoints(int points = 1)
    {
        ActionPoints -= points;
        onActionPointsChanged.Invoke(ActionPoints);
        if (ActionPoints > 0) return;
        
        SelectWeapon(WeaponType.None);
        _gameManager.PlayerEndTurn();
    }
}
