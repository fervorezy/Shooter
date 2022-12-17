using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WeaponSound))]
[RequireComponent(typeof(WeaponAnimator))]
public class Weapon : ObjectPool<Bullet>
{
	[SerializeField] private WeaponData _data;
    [SerializeField] private Transform _shootPoint;

    private int _bulletCount;
    private int _maxBulletCount;
    private int _totalBulletCount;
    private bool _reloading = false;
    private WeaponAnimator _animator;

    public WeaponData Data => _data;
    public bool IsEmptyMagazine => _bulletCount == 0;

    public event UnityAction<int, int> BulletCountChanged;
    public event UnityAction<AudioClip> ActionSoundStarted;

    public void Awake()
    {
        _animator = GetComponent<WeaponAnimator>();

        _maxBulletCount = _data.BulletCount;
        _bulletCount = _maxBulletCount;
        _totalBulletCount = _bulletCount * 10;

        string bulletPoolContainerName = _data.Name + "_BulletPoolContainer";
        GameObject bulletPoolContainer = new GameObject(bulletPoolContainerName);

        Initialize(_data.Bullet, bulletPoolContainer.transform, _data.PoolBulletCount);
    }

    private void OnEnable()
    {
        BulletCountChanged?.Invoke(_bulletCount, _totalBulletCount);
    }

    private void Start()
    {
        BulletCountChanged?.Invoke(_bulletCount, _totalBulletCount);
    }

    public bool TryShoot()
    {
        if (CanShoot())
        {
            Shoot();

            return true;
        }

        return false;
    }

    private void Shoot()
	{
        if (TryGetDisabledObject(out Bullet bullet))
        {
            bullet.transform.position = _shootPoint.position;
            bullet.transform.rotation = _shootPoint.rotation;
            bullet.gameObject.SetActive(true);

            _bulletCount--;

            ActionSoundStarted?.Invoke(_data.Shot);
            _animator.PlayAnimation(_data.ShotAnimationName);
            BulletCountChanged?.Invoke(_bulletCount, _totalBulletCount);
        }
    }

    private bool CanShoot()
    {
        return _bulletCount > 0 && _reloading == false;
    }

    public bool TryReload()
    {
        if (CanReload())
        {
            Reload();

            return true;
        }

        return false;
    }

    private void Reload()
    {
        _reloading = true;

        if (IsEmptyMagazine)
        {
            ActionSoundStarted?.Invoke(_data.ReloadEmpty);
            _animator.PlayAnimation(_data.ReloadEmptyAnimationName);
        }
        else
        {
            ActionSoundStarted?.Invoke(_data.Reload);
            _animator.PlayAnimation(_data.ReloadAnimationName);
        }
    }

    private bool CanReload()
    {
        return _totalBulletCount > 0 && _bulletCount < _maxBulletCount && _reloading == false;
    }

    public void OnAmmoLoaded()
    {
        int loadedBulletCount = _maxBulletCount - _bulletCount;

        if (loadedBulletCount <= _totalBulletCount)
        {
            _totalBulletCount -= loadedBulletCount;
            _bulletCount += loadedBulletCount;
        }
        else
        {
            _bulletCount += _totalBulletCount;
            _totalBulletCount = 0;
        }

        BulletCountChanged?.Invoke(_bulletCount, _totalBulletCount);
    }

    public void OnWeaponReloadEnded()
    {
        _reloading = false;
    }

    public void OnEjectCasing()
    {

    }

    public void OnSlideBack()
    {

    }
}