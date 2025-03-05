using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterSpawnpoint : MonoBehaviour
{
    public GameObject shooterPrefab;
    private float timer = 0f;
    public float spawnInterval = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnInterval)
        {
            Spawn();
            timer = 0f;
        }

        
    }

    public void Spawn()
    {
        if (shooterPrefab != null)
        {
            {
                GameObject shooter = Instantiate(shooterPrefab, transform.position, transform.rotation);
            }
        }
    }
}

