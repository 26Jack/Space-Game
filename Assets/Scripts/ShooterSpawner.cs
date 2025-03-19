using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterSpawner : MonoBehaviour
{
    public GameObject shooterPrefab;
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
            if (!canSpawn) {
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
        if (shooterPrefab != null)
        {
            {
                GameObject shooter = Instantiate(shooterPrefab, transform.position, transform.rotation);
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
                spawnInterval = 5;
                spawnIncreaseInterval = 5.1f;
                spawnSpeedUp = 0.1f;
                timer = 2.5f;
                canSpawn = true;
                break;
            case 5:
                spawnInterval = 3;
                spawnIncreaseInterval = 5.5f;
                spawnSpeedUp = 0.15f;
                timer = 0f;
                canSpawn = true;
                break;
            case 6:
                spawnInterval = 7.5f;
                spawnIncreaseInterval = 8f;
                spawnSpeedUp = 0.3f;
                timer = 5f;
                canSpawn = true;
                break;
            case 7:
                spawnInterval = 0;
                spawnIncreaseInterval = 0f;
                spawnSpeedUp = 0f;
                timer = 0f;
                canSpawn = false;
                break;
            case 8:
                spawnInterval = 0;
                spawnIncreaseInterval = 0f;
                spawnSpeedUp = 0f;
                timer = 0f;
                canSpawn = false;
                break;
            case 9:
                spawnInterval = 7.5f;
                spawnIncreaseInterval = 8f;
                spawnSpeedUp = 0.3f;
                timer = 0f;
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
                spawnInterval = 5f;
                spawnIncreaseInterval = 5.1f;
                spawnSpeedUp = 0.25f;
                timer = 0f;
                canSpawn = true;
                break;
            case 12:
                spawnInterval = 6.8f;
                spawnIncreaseInterval = 6.1f;
                spawnSpeedUp = 0.4f;
                timer = 1f;
                break;
            case 13:
                canSpawn = false;
                break;
        }
    }
}

