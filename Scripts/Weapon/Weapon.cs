using UnityEngine;
using UnityEngine.Events;

public class Weapon : ObjectPool<Bullet>
{
	[SerializeField] protected WeaponData WeaponData;

    private int _maxBulletCount;
    private int _currentBulletCount;
    private int _totalBulletCount;

    public event UnityAction<int, int> BulletCountChanged;

    public void Awake()
    {
        _maxBulletCount = WeaponData.BulletCount;
        _currentBulletCount = _maxBulletCount;
        _totalBulletCount = _currentBulletCount * 2;

        Initialize(WeaponData.Bullet, WeaponData.BulletCount);
    }

    private void Start()
    {
        BulletCountChanged?.Invoke(_currentBulletCount, _totalBulletCount);
    }

    public void Shoot(Transform shootPoint)
	{
        if (_currentBulletCount > 0)
        {
            if (TryGetDisabledObject(out Bullet bullet))
            {
                bullet.transform.position = shootPoint.position;
                bullet.transform.rotation = shootPoint.rotation;
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