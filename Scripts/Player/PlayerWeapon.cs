using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Weapon _startWeapon;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private Weapon _secondWeapon;

    private Weapon _currentWeapon;
    private int _currentWeaponNumber = 0;
    private List<Weapon> _weapons = new List<Weapon>();

    public event UnityAction<Weapon> WeaponChanged;

    private void Awake()
    {
        _currentWeapon = Instantiate(_startWeapon, _weaponPoint.position,
            Quaternion.identity, _weaponPoint.transform);
        _weapons.Add(_currentWeapon);

        _secondWeapon = Instantiate(_secondWeapon, _weaponPoint.position,
            Quaternion.identity, _weaponPoint.transform);
        _secondWeapon.gameObject.SetActive(false);
        _weapons.Add(_secondWeapon);
    }

    private void Start()
    {
        WeaponChanged?.Invoke(_currentWeapon);
    }

    public void Shoot()
    {
        _currentWeapon.Shoot();
    }

    public void Reload()
    {
        _currentWeapon.Reload();
    }

    public void SelectNextWeapon()
    {
        if (_currentWeaponNumber == _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

        SetCurrentWeapon(_weapons[_currentWeaponNumber]);
    }

    public void SelectPreviousWeapon()
    {
        if (_currentWeaponNumber == 0)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        SetCurrentWeapon(_weapons[_currentWeaponNumber]);
    }

    public void SelectPistol()
    {
        string weaponName = "Pistol";

        SelectWeapon(weaponName);
    }

    public void SelectSMG()
    {
        string weaponName = "SMG";

        SelectWeapon(weaponName);
    }

    private void SelectWeapon(string name)
    {
        if (_currentWeapon.Data.Name != name)
        {
            foreach (Weapon weapon in _weapons)
            {
                if (weapon.Data.Name == name)
                    SetCurrentWeapon(weapon);
            }
        }
    }

    private void SetCurrentWeapon(Weapon weapon)
    {
        _currentWeapon.gameObject.SetActive(false);

        _currentWeapon = weapon;
        WeaponChanged?.Invoke(_currentWeapon);
        _currentWeapon.gameObject.SetActive(true);
    }
}