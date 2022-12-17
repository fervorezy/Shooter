using UnityEngine;

[RequireComponent(typeof(CharacterLook))]
[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterWeapon))]
public class CharacterInput : MonoBehaviour
{
    private PlayerInput _input;
    private CharacterLook _visibility;
    private CharacterMovement _movement;
    private CharacterWeapon _weapon;
    private Vector2 _direction;
    private Vector2 _rotate;
    private float mouseScroll;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Enable();

        _visibility = GetComponent<CharacterLook>();
        _movement = GetComponent<CharacterMovement>();
        _weapon = GetComponent<CharacterWeapon>();
    }

    private void Start()
    {
        _input.Player.Shoot.performed += ctx => _weapon.TryShoot();
        _input.Player.Reload.performed += ctx => _weapon.TryReload();
        _input.Player.SelectWeapon.performed += ctx => mouseScroll = ctx.ReadValue<float>();
        _input.Player.SelectWeaponPistol.performed += ctx => _weapon.SelectPistol();
        _input.Player.SelectWeaponSMG.performed += ctx => _weapon.SelectSMG();
    }

    private void Update()
    {
        _rotate = _input.Player.Look.ReadValue<Vector2>();
        _direction = _input.Player.Move.ReadValue<Vector2>();

        _visibility.Look(_rotate);
        _movement.Move(_direction);

        if (mouseScroll > 0)
            _weapon.SelectNextWeapon();
        else if (mouseScroll < 0)
            _weapon.SelectPreviousWeapon();
    }
}