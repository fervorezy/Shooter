using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
	[SerializeField] private string _shotStateName;
    [SerializeField] private float _shotCrossFadeDuration;

    [SerializeField] private string _reloadStateName;
	[SerializeField] private string _reloadEmptyStateName;

	[SerializeField] private string _overlayLayerName;
	[SerializeField] private string _actionsLayerName;

	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public void PlayShotAnimation()
	{
		int overlayLayerIndex = _animator.GetLayerIndex(_overlayLayerName);

		_animator.CrossFade(_shotStateName, _shotCrossFadeDuration,
			overlayLayerIndex, 0f);
	}

	public void PlayReloadAnimation(bool isEmptyWeaponMagazine)
	{
		PlayActionAnimation(isEmptyWeaponMagazine ? _reloadEmptyStateName : _reloadStateName);
    }

	private void PlayActionAnimation(string stateName)
	{
        int actionsLayerIndex = _animator.GetLayerIndex(_actionsLayerName);

		_animator.Play(stateName, actionsLayerIndex, 0f);
    }
}