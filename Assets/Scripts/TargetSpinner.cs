using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpinner : MonoBehaviour
{
    float randomRotation;

    public float timer = 0f;
    public float rotationInterval;

    public float randomMin = 1f;
    public float randomMax = 4f;

    // Start is called before the first frame update
    void Start()
    {
        rotationInterval = Random.Range(randomMin, randomMax);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > rotationInterval)
        {
            randomRotation = Random.Range(0f, 360f);
            transform.Rotate(0f, 0f, randomRotation);
            timer = 0f;
            rotationInterval = Random.Range(1f, 4f);
        }
    }
}
