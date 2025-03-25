using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuThrust : MonoBehaviour
{
    public AudioClip thrustSound;
    public float thrustMaxLoudness = 0.2f;
    public float distance;
    public float thrustLoudness;
    private AudioSource thrustAudioSource;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target")?.transform;

        thrustAudioSource = gameObject.AddComponent<AudioSource>();
        thrustAudioSource.loop = false;
        thrustAudioSource.clip = thrustSound;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target.position);
            thrustLoudness = thrustMaxLoudness - (distance / 200) * 0.3f;
            if (thrustLoudness > thrustMaxLoudness)
            {
                thrustLoudness = thrustMaxLoudness;
            }
        }

        if (!thrustAudioSource.isPlaying)
        {
            thrustAudioSource.volume = thrustLoudness;
            thrustAudioSource.Play();
        }
    }
}
