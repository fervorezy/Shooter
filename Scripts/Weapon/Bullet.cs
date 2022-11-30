using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private BulletData _bulletData;

	private float _pastDistance;

	private void Update()
	{
		Vector3 lastPosition = transform.position;

        transform.Translate(transform.forward * _bulletData.Speed
			* Time.deltaTime, Space.World);

		float pastDistanceMagnitude = (transform.position - lastPosition).magnitude;
        _pastDistance += Mathf.Abs(pastDistanceMagnitude);

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