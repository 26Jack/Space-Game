using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestScore : MonoBehaviour
{
    public GameObject digitPrefab;
    public int spacing = 16;
    private List<GameObject> digits = new List<GameObject>();

    public int bestScore = 0;
    public int currentScore = 0;

    public void Start()
    {
        bestScore = PlayerPrefs.GetInt("Best", 0); // retrieve saved best score
        UpdateScore(bestScore);
    }

    public void UpdateScore(int score)
    {
        currentScore = score;

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("Best", bestScore);
            PlayerPrefs.Save();
        }

        ClearDigits();
        string scoreStr = bestScore.ToString();
        int length = scoreStr.Length;

        for (int i = 0; i < length; i++)
        {
            int digitValue = scoreStr[i] - '0'; // convert char to int
            GameObject newDigit = Instantiate(digitPrefab, transform);
            newDigit.transform.localPosition = new Vector3((length - 1 - i) * -spacing, 0, 0);
            newDigit.GetComponent<ScoreDigit>().SetDigit(digitValue);
            digits.Add(newDigit);
        }
    }

    private void ClearDigits()
    {
        foreach (GameObject digit in digits)
        {
            Destroy(digit);
        }
        digits.Clear();
    }
}
