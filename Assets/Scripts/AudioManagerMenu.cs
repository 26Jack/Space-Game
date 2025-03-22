using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMenu : MonoBehaviour
{
    public static AudioManagerMenu Instance;

    private AudioSource audioSource;
    private AudioSource loopAudioSource;

    // To run in another script:
    // AudioManagerMenu.Instance.PlaySound(AudioManagerMenu.Instance.shooterShoot);

    public AudioClip flashOff;
    public AudioClip flashOn;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        loopAudioSource = gameObject.AddComponent<AudioSource>();

        loopAudioSource.loop = true;
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }

    public void StartLoopingSound(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
        {
            if (loopAudioSource.clip != clip)
            {
                loopAudioSource.clip = clip;
                loopAudioSource.volume = volume;
                loopAudioSource.Play();
            }
        }
    }

    public void StopLoopingSound()
    {
        loopAudioSource.Stop();
    }
}
