using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveOnKey : MonoBehaviour
{
    public KeyCode activationKey = KeyCode.W;
    public KeyCode activationKey2 = KeyCode.UpArrow;

    public float emission = 20f;

    private ParticleSystem particleEffect;
    private ParticleSystem.EmissionModule emissionModule;

    private void Start()
    {
        particleEffect = GetComponent<ParticleSystem>();
        emissionModule = particleEffect.emission;
    }

    void Update()
    {
        if (Input.GetKey(activationKey) || Input.GetKey(activationKey2))
        {
            emissionModule.rateOverTime = emission;
        }
        else
        {
            emissionModule.rateOverTime = 0f;
        }
    }
}
