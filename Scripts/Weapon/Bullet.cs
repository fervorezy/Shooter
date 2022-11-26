using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private BulletData _bulletData;

	private float _speed;
	private Coroutine _currentCoroutine;

	public BulletData Data => _bulletData;

	private void Start()
	{
		_speed = _bulletData.Speed;
	}

	private void OnTriggerEnter(Collider other)
	{
		gameObject.SetActive(false);
	}

	public void StartMove()
	{
		StopCurrentCoroutine();
		_currentCoroutine = StartCoroutine(Move());
	}

	private IEnumerator Move()
	{
		Vector3 targetPosition = new Vector3(0, 0, _bulletData.FlightRange);

		while (transform.position.z < _bulletData.FlightRange)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPosition,
				_speed * Time.deltaTime);

			yield return null;
		}

		gameObject.SetActive(false);
	}

	private void StopCurrentCoroutine()
	{
		if (_currentCoroutine != null)
			StopCoroutine(_currentCoroutine);
	}
}