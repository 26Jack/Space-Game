using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.restartEnd, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.restartStart, 0.5f);

        // start fade effect before restarting
        ScreenFader.Instance.FadeToBlackAndRestart();
    }
}
