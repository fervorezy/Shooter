using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
	[SerializeField] private int _maxHealth;
	[SerializeField] private PlayerLook _playerLook;
	[SerializeField] private PlayerMovement _playerMovement;
	[SerializeField] private Weapon _startWeapon;
	[SerializeField] private Transform _weaponPoint;
	[SerializeField] private Transform _shootPoint;

	private PlayerInput _input;
	private Vector2 _direction;
	private Vector2 _rotate;
	private int _currentHealth;
	private Weapon _currentWeapon;
	private List<Weapon> _weapons = new List<Weapon>();

	public event UnityAction<int> HealthChanged;
	public event UnityAction Died;

	private void Awake()
	{
		_input = new PlayerInput();
		_input.Enable();

		_input.Player.Shoot.performed += ctx => Shoot();
	}

	private void Start()
	{
		_currentHealth = _maxHealth;
		HealthChanged?.Invoke(_currentHealth);

		_currentWeapon = Instantiate(_startWeapon, _weaponPoint.position,
			Quaternion.identity, _weaponPoint.transform);
		_weapons.Add(_currentWeapon);
	}

	private void Update()
	{
        _direction = _input.Player.Move.ReadValue<Vector2>();
        _rotate = _input.Player.Look.ReadValue<Vector2>();

		_playerMovement.Move(_direction);
		_playerLook.Look(_rotate);
    }

	public void TakeDamage(int damage)
	{
		_currentHealth -= damage;
		HealthChanged?.Invoke(_currentHealth);

		if (_currentHealth <= 0)
			Die();
	}

	private void Shoot()
	{
		_currentWeapon.Shoot(_shootPoint);
	}

	private void Die()
	{
		Died?.Invoke();
	}
}