using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WeaponSound))]
[RequireComponent(typeof(WeaponAnimator))]
public class Weapon : ObjectPool<Bullet>
{
	[SerializeField] private WeaponData _weaponData;
    [SerializeField] private Transform _shootPoint;

    private int _maxBulletCount;
    private int _currentBulletCount;
    private int _totalBulletCount;
    private WeaponAnimator _animator;

    public WeaponData Data => _weaponData;

    public event UnityAction<int, int> BulletCountChanged;
    public event UnityAction<AudioClip> ActionSoundStarted;

    public void Awake()
    {
        _animator = GetComponent<WeaponAnimator>();

        _maxBulletCount = _weaponData.BulletCount;
        _currentBulletCount = _maxBulletCount;
        _totalBulletCount = _currentBulletCount * 10;

        string bulletPoolContainerName = _weaponData.Name + "_BulletPoolContainer";
        GameObject bulletPoolContainer = new GameObject(bulletPoolContainerName);

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

                ActionSoundStarted?.Invoke(_weaponData.Shot);
                _animator.PlayAnimation(_weaponData.ShotAnimationName);
                BulletCountChanged?.Invoke(_currentBulletCount, _totalBulletCount);
            }
        }
        else
        {
            // need coroutine or replace reload mb
            ActionSoundStarted?.Invoke(_weaponData.ShotEmpty);
            Reload();
        }
    }

    public void Reload()
    {
        if (_totalBulletCount > 0 && _currentBulletCount < _maxBulletCount)
        {
            int loadedBulletCount = _maxBulletCount - _currentBulletCount;

            LoadAmmo(loadedBulletCount);

            if (loadedBulletCount == _maxBulletCount)
            {
                ActionSoundStarted?.Invoke(_weaponData.ReloadEmpty);
                _animator.PlayAnimation(_weaponData.ReloadEmptyAnimationName);
            }
            else
            {
                ActionSoundStarted?.Invoke(_weaponData.Reload);
                _animator.PlayAnimation(_weaponData.ReloadAnimationName);
            }

            BulletCountChanged?.Invoke(_currentBulletCount, _totalBulletCount);
        }
    }

    private void LoadAmmo(int bulletCount)
    {
        if (bulletCount <= _totalBulletCount)
        {
            _totalBulletCount -= bulletCount;
            _currentBulletCount += bulletCount;
        }
        else
        {
            _currentBulletCount += _totalBulletCount;
            _totalBulletCount = 0;
        }
    }
}