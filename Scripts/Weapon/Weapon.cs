using UnityEngine;
using UnityEngine.Events;

public class Weapon : ObjectPool<Bullet>
{
	[SerializeField] private WeaponData _weaponData;
    [SerializeField] private Transform _shootPoint;

    private int _maxBulletCount;
    private int _currentBulletCount;
    private int _totalBulletCount;
    private string _bulletPoolContainerName;

    public WeaponData Data => _weaponData;

    public event UnityAction<int, int> BulletCountChanged;

    public void Awake()
    {
        _maxBulletCount = _weaponData.BulletCount;
        _currentBulletCount = _maxBulletCount;
        _totalBulletCount = _currentBulletCount * 2;

        _bulletPoolContainerName = _weaponData.Name + "-BulletPoolContainer";
        GameObject bulletPoolContainer = new GameObject(_bulletPoolContainerName);
        Initialize(_weaponData.Bullet, bulletPoolContainer.transform, _weaponData.PoolBulletCount);
    }

    private void OnEnable()
    {
        BulletCountChanged?.Invoke(_currentBulletCount, _totalBulletCount);
    }

    private void Start()
    {
        BulletCountChanged?.Invoke(_currentBulletCount, _totalBulletCount);
    }

    public void Shoot()
	{
        if (_currentBulletCount > 0)
        {
            if (TryGetDisabledObject(out Bullet bullet))
            {
                bullet.transform.position = _shootPoint.position;
                bullet.transform.rotation = _shootPoint.rotation;
                bullet.gameObject.SetActive(true);

                _currentBulletCount--;
                BulletCountChanged?.Invoke(_currentBulletCount, _totalBulletCount);
            }
        }
        else
            Reload();
    }

    public void Reload()
    {
        if (_totalBulletCount > 0 && _currentBulletCount < _maxBulletCount)
        {
            // reload animation

            int loadedBulletCount = _maxBulletCount - _currentBulletCount;

            if (loadedBulletCount <= _totalBulletCount)
            {
                _totalBulletCount -= loadedBulletCount;
                _currentBulletCount += loadedBulletCount;
            }
            else
            {
                _currentBulletCount += _totalBulletCount;
                _totalBulletCount = 0;
            }
            
            BulletCountChanged?.Invoke(_currentBulletCount, _totalBulletCount);
            Debug.Log("Reload");
        }
        //else
        //    animation or sound and no ammo message
    }
}