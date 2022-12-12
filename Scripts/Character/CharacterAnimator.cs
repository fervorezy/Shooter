using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterWeapon))]
public class CharacterAnimator : MonoBehaviour
{
	[SerializeField] private string _shotStateName;
    [SerializeField] private float _shotCrossFadeDuration;

    [SerializeField] private string _reloadStateName;

	[SerializeField] private string _overlayLayerName;
	[SerializeField] private string _actionsLayerName;

	private Animator _animator;
	private CharacterWeapon _weapon;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_weapon = GetComponent<CharacterWeapon>();
	}

	public void PlayShotAnimation()
	{
		int overlayLayerIndex = _animator.GetLayerIndex(_overlayLayerName);

		_animator.CrossFade(_shotStateName, _shotCrossFadeDuration,
			overlayLayerIndex, 0f);
	}

	public void PlayReloadAnimation()
	{
		int actionsLayerIndex = _animator.GetLayerIndex(_actionsLayerName);

		_animator.Play(_reloadStateName, actionsLayerIndex, 0f);
	}
}