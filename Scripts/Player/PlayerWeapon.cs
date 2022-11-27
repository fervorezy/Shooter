using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Weapon _startWeapon;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private Transform _shootPoint;

    private Weapon _currentWeapon;
    private List<Weapon> _weapons = new List<Weapon>();

    private void Start()
    {
        _currentWeapon = Instantiate(_startWeapon, _weaponPoint.position,
            Quaternion.identity, _weaponPoint.transform);
        _weapons.Add(_currentWeapon);
    }

    public void Shoot()
    {
        _currentWeapon.Shoot(_shootPoint);
    }
}