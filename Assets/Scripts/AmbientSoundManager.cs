using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientNoiseManager : MonoBehaviour
{
    public int soundID = 1;

    public float timer = 0;
    public float ambienceInterval;
    public float intervalMin = 5;
    public float intervalMax = 20;

    // Start is called before the first frame update
    void Start()
    {
        ambienceInterval = Random.Range(intervalMin, intervalMax);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= ambienceInterval)
        {
            PlayAmbience();
            ambienceInterval = Random.Range(intervalMin, intervalMax);

            timer = 0;
        }
    }

    public void PlayAmbience()
    {
        soundID = Random.Range(1, 14);

        switch (soundID)
        {
            case 1:
                AudioManager.Instance.PlaySound(AudioManager.Instance.ambience1, 0.12f);
                break;
            case 2:
                AudioManager.Instance.PlaySound(AudioManager.Instance.ambience2, 0.1f);
                break;
            case 3:
                AudioManager.Instance.PlaySound(AudioManager.Instance.ambience3, 0.1f);
                break;
            case 4:
                AudioManager.Instance.PlaySound(AudioManager.Instance.ambience4, 0.1f);
                break;
            case 5:
                AudioManager.Instance.PlaySound(AudioManager.Instance.ambience5, 0.1f);
                break;
            case 6:
                AudioManager.Instance.PlaySound(AudioManager.Instance.ambience6, 0.1f);
                break;
            case 7:
                PlayAmbience();
                break;
            case 8:
                PlayAmbience();
                break;
            case 9:
                PlayAmbience();
                break;
            case 10:
                PlayAmbience();
                break;
            case 11:
                PlayAmbience();
                break;
            case 12:
                PlayAmbience();
                break;
            case 13:
                PlayAmbience();
                break;
        }
    }
}
