using UnityEngine;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
	[SerializeField] private TMP_Text _currentBulletCountDisplay;
	[SerializeField] private TMP_Text _totalBulletCountDisplay;
	[SerializeField] private PlayerWeapon _playerWeapon;

	private void OnEnable()
	{
		_playerWeapon.CurrentWeapon.BulletCountChanged += Render;
	}

	private void OnDisable()
	{
        _playerWeapon.CurrentWeapon.BulletCountChanged -= Render;
    }

	public void Render(int currentBulletCount, int totalBulletCount)
	{
		_currentBulletCountDisplay.text = currentBulletCount.ToString();
        _totalBulletCountDisplay.text = totalBulletCount.ToString();
    }
}