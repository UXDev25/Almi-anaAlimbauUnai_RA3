
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;

	[Header("Audio Sources")]
	[SerializeField] private AudioSource musicSource;

	[SerializeField] private int initialSize = 5;
	private Stack<AudioSource> pool = new Stack<AudioSource>();

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);

		for (int i = 0; i < initialSize; i++) 
		{
			//pool.Push();
		}
	}

	public void PlaySound(AudioClip clip, bool loop = true, float volume = 1f)
	{
		if (pool.Count <= 0) gameObject.AddComponent<AudioSource>();
		AudioSource newSource = pool.Pop();
		newSource.volume = volume;
		newSource.enabled = true;
		if (loop) 
		{
			newSource.clip = clip;
			newSource.loop = true;
			newSource.Play();
			return;
		}
		newSource.PlayOneShot(clip);
	}

	public void StopMusic()
	{
		musicSource.Stop();
	}
}