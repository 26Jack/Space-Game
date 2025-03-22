using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTracker : MonoBehaviour
{
    public int chasersKilled = 0;
    public int wanderersKilled = 0;
    public int shootersKilled = 0;
    public int driftersKilled = 0;

    public ScoreDisplay chaserDisplay;
    public ScoreDisplay wandererDisplay;
    public ScoreDisplay shooterDisplay;
    public ScoreDisplay drifterDisplay;

    // Start is called before the first frame update
    void Start()
    {
        UpdateKillDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChaserKilled()
    {
        chasersKilled++;
        //Debug.Log(chasersKilled);
        UpdateKillDisplay();
    }

    public void WandererKilled()
    {
        wanderersKilled++;
        //Debug.Log(wanderersKilled);
        UpdateKillDisplay();
    }

    public void ShooterKilled()
    {
        shootersKilled++;
        //Debug.Log(shootersKilled);
        UpdateKillDisplay();
    }

    public void DrifterKilled()
    {
        driftersKilled++;
        //Debug.Log(driftersKilled);
        UpdateKillDisplay();
    }

    public void GameOver()
    {
        // show kill amounts
    }

    private void UpdateKillDisplay()
    {
        chaserDisplay.UpdateScore(chasersKilled);
        wandererDisplay.UpdateScore(wanderersKilled);
        shooterDisplay.UpdateScore(shootersKilled);
        drifterDisplay.UpdateScore(driftersKilled);
    }
}
