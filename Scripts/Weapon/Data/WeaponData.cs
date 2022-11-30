using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Objects/Weapon")]
public class WeaponData : ScriptableObject
{
	[SerializeField] private string _name;
    [SerializeField] private int _poolBulletCount;
    [SerializeField] private int _bulletCount;
	[SerializeField] private float _reloadTime;
	[SerializeField] private float _rateFire;
	[SerializeField] private Bullet _bullet;

    public string Name => _name;
    public int PoolBulletCount => _poolBulletCount;
    public int BulletCount => _bulletCount;
	public float ReloadTime => _reloadTime;
	public float RateFire => _rateFire;
	public Bullet Bullet => _bullet;
}