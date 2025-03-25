using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    private static VolumeManager instance; // please work
    private float volume = 1.0f;
    private const float minVolume = 0.0f;
    private const float maxVolume = 1.5f;
    private const float volumeStep = 0.1f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // please work
    }

    // Start is called before the first frame update
    void Start()
    {
        volume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        SetVolume(volume);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period)) ChangeVolume(volumeStep);
        else if (Input.GetKeyDown(KeyCode.Comma)) ChangeVolume(-volumeStep);
    }

    void ChangeVolume(float amount)
    {
        volume = Mathf.Clamp(volume + amount, minVolume, maxVolume);
        SetVolume(volume);
    }

    void SetVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
        PlayerPrefs.SetFloat("MasterVolume", newVolume);
        PlayerPrefs.Save();
        Debug.Log(newVolume * 100);
    }
}