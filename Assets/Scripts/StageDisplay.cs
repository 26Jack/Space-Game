using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDisplay : MonoBehaviour
{
    public GameObject digitPrefab;
    public int spacing = 16;
    private List<GameObject> digits = new List<GameObject>();

    public void UpdateStage(int stage)
    {
        ClearDigits();
        string stageStr = stage.ToString();
        int length = stageStr.Length;

        for (int i = 0; i < length; i++)
        {
            int digitValue = stageStr[i] - '0'; // Convert char to int
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
