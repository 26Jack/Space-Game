using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource audioSource;
    private AudioSource loopAudioSource;


    // To run in another script:
    // AudioManager.Instance.PlaySound(AudioManager.Instance.shooterShoot);

    public AudioClip playerShoot;
    public AudioClip playerHit;
    public AudioClip playerDie;

    public AudioClip playerThrust;

    public AudioClip ammoRecharge1;
    public AudioClip ammoRecharge2;
    public AudioClip ammoRecharge3;
    public AudioClip outOfAmmo; // make logic in player controller for this
    public AudioClip wallHit;

    public AudioClip shooterShoot;
    public AudioClip drifterShoot;

    public AudioClip chaserDie;
    public AudioClip wandererDie;
    public AudioClip shooterDie;
    public AudioClip drifterDie;

    public AudioClip stageAdvance;

    public AudioClip restartStart;
    public AudioClip restartEnd;

    public AudioClip points1;
    public AudioClip points2;
    public AudioClip points3;

    public AudioClip chaserPing;
    public AudioClip wandererPing;
    public AudioClip shooterPing;
    public AudioClip drifterPing;
    // sounds for thrusting and turning would be cool but id need more logic for those


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
