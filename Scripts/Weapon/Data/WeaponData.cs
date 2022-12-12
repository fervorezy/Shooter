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

    [SerializeField] private AudioClip _shot;
	[SerializeField] private AudioClip _shotEmpty;
	[SerializeField] private AudioClip _reload;
    [SerializeField] private AudioClip _reloadEmpty;
	[SerializeField] private AudioClip _holster;
	[SerializeField] private AudioClip _unholster;

	[SerializeField] private string _shotAnimationName;
	[SerializeField] private string _reloadAnimationName;
	[SerializeField] private string _reloadEmptyAnimationName;

    public string Name => _name;
    public int PoolBulletCount => _poolBulletCount;

    public int BulletCount => _bulletCount;
	public float ReloadTime => _reloadTime;
	public float RateFire => _rateFire;
    public Bullet Bullet => _bullet;

    public AudioClip Shot => _shot;
	public AudioClip ShotEmpty => _shotEmpty;
	public AudioClip Reload => _reload;
	public AudioClip ReloadEmpty => _reloadEmpty;
	public AudioClip Holster => _holster;
	public AudioClip Unholster => _unholster;

	public string ShotAnimationName => _shotAnimationName;
	public string ReloadAnimationName => _reloadAnimationName;
	public string ReloadEmptyAnimationName => _reloadEmptyAnimationName;
}