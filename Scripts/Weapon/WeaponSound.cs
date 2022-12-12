using UnityEngine;

[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(AudioSource))]
public class WeaponSound : MonoBehaviour
{
	private Weapon _weapon;
	private AudioSource _audioSource;

	private void Awake()
	{
		_weapon = GetComponent<Weapon>();
		_audioSource = GetComponent<AudioSource>();
	}

	private void OnEnable()
	{
		_weapon.ActionSoundStarted += PlaySound;
	}

	private void OnDisable()
	{
        _weapon.ActionSoundStarted -= PlaySound;
    }

	private void PlaySound(AudioClip sound)
	{
		_audioSource.clip = sound;
		_audioSource.Play();
	}
}