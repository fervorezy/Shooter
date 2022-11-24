using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
	[SerializeField] private int _currentHealth;
	[SerializeField] private PlayerLook _playerLook;
	[SerializeField] private PlayerMovement _playerMovement;

	private PlayerInput _input;
	private Vector2 _direction;
	private Vector2 _rotate;

	public event UnityAction<int> HealthChanged;
	public event UnityAction Died;

	private void Awake()
	{
		_input = new PlayerInput();
		_input.Enable();
	}

	private void Start()
	{
		HealthChanged?.Invoke(_currentHealth);
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

	private void Die()
	{
		Died?.Invoke();
	}
}