using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WeaponAnimator : MonoBehaviour
{
	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public void PlayAnimation(string animationName)
	{
		int layer = 0;
		float normalizedTime = 0f;

		_animator.Play(animationName, layer, normalizedTime);
	}
}