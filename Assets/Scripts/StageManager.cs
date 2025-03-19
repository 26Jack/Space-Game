using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public float timer = 0f;
    public float stageUpdateInterval = 50f;

    public int currentStage = 1;

    public bool playerAlive = true;

    public ChaserSpawner chaserSpawner;
    public ShooterSpawner shooterSpawner;
    public WandererSpawner wandererSpawner;
    public DrifterSpawner drifterSpawner;

    public StageDisplay stageDisplay;

    public GameObject advanceFlash;

    public EmissionController emissionController;

    // Start is called before the first frame update
    void Start()
    {
        if (chaserSpawner == null)
        {
            chaserSpawner = FindObjectOfType<ChaserSpawner>();
        }
        if (shooterSpawner == null)
        {
            shooterSpawner = FindObjectOfType<ShooterSpawner>();
        }
        if (wandererSpawner == null)
        {
            wandererSpawner = FindObjectOfType<WandererSpawner>();
        }
        if (drifterSpawner == null)
        {
            drifterSpawner = FindObjectOfType<DrifterSpawner>();
        }

        if (stageDisplay == null)
        {
            stageDisplay = FindObjectOfType<StageDisplay>();
        }

        if (emissionController == null)
        {
            emissionController = FindObjectOfType<EmissionController>();
        }

        updateAll();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAlive)
        {
            timer += Time.deltaTime;

            if (timer >= stageUpdateInterval)
            {
                if (currentStage < 12)
                {
                    timer = 0;
                    currentStage++;
                    AudioManager.Instance.PlaySound(AudioManager.Instance.stageAdvance);
                    GameObject notif = Instantiate(advanceFlash, transform.position, Quaternion.identity);
                    updateAll();
                }
            }
        } else
        {
            currentStage = 13;
            pauseStages();
        }
    }

    public void updateAll()
    {
        stageDisplay.UpdateStage(currentStage);

        chaserSpawner.LevelUpdate(currentStage);
        shooterSpawner.LevelUpdate(currentStage);
        wandererSpawner.LevelUpdate(currentStage);
        drifterSpawner.LevelUpdate(currentStage);

        emissionController.SetEmissionRate(currentStage);
        emissionController.SetRandomColor(currentStage);
    }

    public void pauseStages()
    {
        chaserSpawner.LevelUpdate(currentStage);
        shooterSpawner.LevelUpdate(currentStage);
        wandererSpawner.LevelUpdate(currentStage);
        drifterSpawner.LevelUpdate(currentStage);
    }

    public void playerDied()
    {
        playerAlive = false;
    }
}
