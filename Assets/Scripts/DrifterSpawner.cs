using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrifterSpawner : MonoBehaviour
{
    public GameObject DrifterPrefab;
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
        if (DrifterPrefab != null)
        {
            {
                GameObject shooter = Instantiate(DrifterPrefab, transform.position, transform.rotation);
                //GameManager.Instance.RegisterObject(shooter);

            }
        }
    }

    public void LevelUpdate(int level)
    {
        switch (level)
        {
            case 1:
                spawnInterval = 0;
                spawnIncreaseInterval = 0f;
                spawnSpeedUp = 0f;
                timer = 0f;
                canSpawn = false;
                break;
            case 2:
                spawnInterval = 0;
                spawnIncreaseInterval = 0f;
                spawnSpeedUp = 0f;
                timer = 0f;
                canSpawn = false;
                break;
            case 3:
                spawnInterval = 0;
                spawnIncreaseInterval = 0f;
                spawnSpeedUp = 0f;
                timer = 0f;
                canSpawn = false;
                break;
            case 4:
                spawnInterval = 0;
                spawnIncreaseInterval = 0f;
                spawnSpeedUp = 0f;
                timer = 0f;
                canSpawn = false;
                break;
            case 5:
                spawnInterval = 0;
                spawnIncreaseInterval = 0f;
                spawnSpeedUp = 0f;
                timer = 0f;
                canSpawn = false;
                break;
            case 6:
                spawnInterval = 0;
                spawnIncreaseInterval = 0f;
                spawnSpeedUp = 0f;
                timer = 0f;
                canSpawn = false;
                break;
            case 7:
                spawnInterval = 6;
                spawnIncreaseInterval = 6.1f;
                spawnSpeedUp = 0.4f;
                timer = 3f;
                canSpawn = true;
                break;
            case 8:
                spawnInterval = 6f;
                spawnIncreaseInterval = 6.1f;
                spawnSpeedUp = 0.1f;
                timer = 1f;
                canSpawn = true;
                break;
            case 9:
                spawnInterval = 6f;
                spawnIncreaseInterval = 6.1f;
                spawnSpeedUp = 0.3f;
                timer = 2f;
                canSpawn = true;
                break;
            case 10:
                spawnInterval = 0;
                spawnIncreaseInterval = 0f;
                spawnSpeedUp = 0f;
                timer = 0f;
                canSpawn = false;
                break;
            case 11:
                spawnInterval = 4f;
                spawnIncreaseInterval = 10f;
                spawnSpeedUp = 0.35f;
                timer = 0f;
                canSpawn = true;
                break;
            case 12:
                spawnInterval = 6.5f;
                spawnIncreaseInterval = 6.6f;
                spawnSpeedUp = 0.3f;
                timer = 1f;
                canSpawn = true;
                break;
            case 13:
                canSpawn = false;
                break;
        }
    }
}
