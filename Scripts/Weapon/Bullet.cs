using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private BulletData _bulletData;

	private float _speed;
	private float _pastDistance;

	public BulletData Data => _bulletData;

	private void Start()
	{
		_speed = _bulletData.Speed;
	}

	private void Update()
	{
		Vector3 lastPosition = transform.position;

        transform.Translate(transform.forward * _speed * Time.deltaTime, Space.World);

        _pastDistance += Mathf.Abs((transform.position - lastPosition).magnitude);

		if (_pastDistance >= _bulletData.FlightRange)
		{
			Destroy();
        }
	}

	private void OnTriggerEnter(Collider other)
	{
		Destroy();
	}

	private void Destroy()
	{
		_pastDistance = 0;
		gameObject.SetActive(false);
    }
}