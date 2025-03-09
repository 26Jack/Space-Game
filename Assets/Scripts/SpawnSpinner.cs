using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpinner : MonoBehaviour
{
    float randomRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        randomRotation = Random.Range(0f, 360f);
        transform.Rotate(0f, 0f, randomRotation);
    }
}
