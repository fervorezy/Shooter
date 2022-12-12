using UnityEngine;
using UnityEngine.Events;

public class CharacterHealth : MonoBehaviour
{
	[SerializeField] private int _maxHealth;

	private int _currentHealth;

	public event UnityAction<int> HealthChanged;
    public event UnityAction Died;

	private void Start()
	{
		_currentHealth = _maxHealth;
		HealthChanged?.Invoke(_currentHealth);
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