using UnityEngine;

public class Weapon : ObjectPool<Bullet>
{
	[SerializeField] protected WeaponData WeaponData;

    public void Start()
    {
        Initialize(WeaponData.Bullet, WeaponData.BulletCount);
    }

    public void Shoot(Transform shootPoint)
	{
        if (TryGetDisabledObject(out Bullet bullet))
        {
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = shootPoint.rotation;
            bullet.gameObject.SetActive(true);
        }
    }
}