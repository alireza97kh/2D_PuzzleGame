using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioManagerData audioManagerData;

	public void PlaySfx(string audioName)
    {
        AudioClip clip = audioManagerData.audioClips.Find(x => x.aucioName == audioName)?.audioClip;
        audioSource.clip = clip;
        audioSource.Play();
	}
}
