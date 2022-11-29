using UnityEngine;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
	[SerializeField] private TMP_Text _currentBulletCountDisplay;
	[SerializeField] private TMP_Text _totalBulletCountDisplay;
	[SerializeField] private PlayerWeapon _playerWeapon;

	private Weapon _currentWeapon;

	private void OnEnable()
	{
        _playerWeapon.WeaponChanged += SetCurrentWeapon;
	}

	private void OnDisable()
	{
        _playerWeapon.WeaponChanged -= SetCurrentWeapon;
    }

	private void SetCurrentWeapon(Weapon weapon)
	{
		if (_currentWeapon != null)
            _currentWeapon.BulletCountChanged -= Render;

        _currentWeapon = weapon;
        _currentWeapon.BulletCountChanged += Render;
    }

	private void Render(int currentBulletCount, int totalBulletCount)
	{
		_currentBulletCountDisplay.text = currentBulletCount.ToString();
        _totalBulletCountDisplay.text = totalBulletCount.ToString();
    }
}