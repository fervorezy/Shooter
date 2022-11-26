using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private BulletData _bulletData;

	private float _speed;
	private float _pastDistance;
	private Vector3 _lastPosition;

	public BulletData Data => _bulletData;

	private void Start()
	{
		_speed = _bulletData.Speed;
		_lastPosition = transform.position;
	}

	private void Update()
	{
        transform.Translate(transform.forward * _speed * Time.deltaTime);

        _pastDistance += Mathf.Abs((transform.position - _lastPosition).magnitude);
		_lastPosition = transform.position;

		if (_pastDistance >= _bulletData.FlightRange)
		{
			_pastDistance = 0;
            gameObject.SetActive(false);
        }
	}

	private void OnTriggerEnter(Collider other)
	{
		gameObject.SetActive(false);
	}
}