using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Objects/Weapon")]
public class WeaponData : ScriptableObject
{
	[SerializeField] private int _bulletCount;
	[SerializeField] private float _reloadTime;
	[SerializeField] private float _rateFire;
	[SerializeField] private Bullet _bullet;

	public int BulletCount => _bulletCount;
	public float ReloadTime => _reloadTime;
	public float RateFire => _rateFire;
	public Bullet Bullet => _bullet;
}