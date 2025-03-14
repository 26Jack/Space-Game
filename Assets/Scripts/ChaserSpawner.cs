using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserSpawner : MonoBehaviour
{
    public GameObject chaserPrefab;
    public float timer = 0f;
    public float spawnInterval = 1f;

    public float spawnIncreaseTimer = 0f;
    public float spawnIncreaseInterval = 15f;
    public float spawnSpeedUp = 0.25f;

    public float maxSpawnRate = 0.3f;

    public bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        spawnIncreaseTimer += Time.deltaTime;

        if (timer > spawnInterval)
        {
            if (!canSpawn)
            {
                return;
            }

            Spawn();
            timer = 0f;
        }

        if (spawnIncreaseTimer > spawnIncreaseInterval)
        {
            if (spawnInterval > maxSpawnRate)
            {
                spawnInterval -= spawnSpeedUp;
                spawnIncreaseTimer = 0f;

                if (spawnInterval < maxSpawnRate)
                { //ugly
                    spawnInterval = maxSpawnRate;
                }
            }

            else
            {
                spawnInterval = maxSpawnRate;
            }

        }

    }

    public void Spawn()
    {
        if (chaserPrefab != null)
        {
            {
                GameObject shooter = Instantiate(chaserPrefab, transform.position, transform.rotation);
                //GameManager.Instance.RegisterObject(shooter);

            }
        }
    }
}
