using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (scoreText == null)
        {
            scoreText = FindObjectOfType<TMP_Text>();
        }

        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
