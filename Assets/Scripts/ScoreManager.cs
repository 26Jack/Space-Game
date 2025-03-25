using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score = 0;
    public ScoreDisplay scoreDisplay;
    public BestScore bestScore;

    // Start is called before the first frame update
    void Start()
    {
        if (scoreText == null)
        {
            scoreText = FindObjectOfType<TMP_Text>();
        }

        if (scoreDisplay == null)
        {
            scoreDisplay = FindObjectOfType<ScoreDisplay>();
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
        scoreDisplay.UpdateScore(score);
        bestScore.UpdateScore(score);
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
