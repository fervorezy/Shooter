using UnityEngine;

[RequireComponent(typeof(PlayerLook))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerWeapon))]
public class Player : MonoBehaviour
{
    private PlayerInput _input;
    private PlayerLook _visibility;
    private PlayerMovement _movement;
    private PlayerWeapon _weapon;
    private Vector2 _direction;
    private Vector2 _rotate;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Enable();

        _visibility = GetComponent<PlayerLook>();
        _movement = GetComponent<PlayerMovement>();
        _weapon = GetComponent<PlayerWeapon>();

        _input.Player.Shoot.performed += ctx => _weapon.Shoot();
    }

    private void Update()
    {
        _rotate = _input.Player.Look.ReadValue<Vector2>();
        _direction = _input.Player.Move.ReadValue<Vector2>();

        _visibility.Look(_rotate);
        _movement.Move(_direction);
    }
}