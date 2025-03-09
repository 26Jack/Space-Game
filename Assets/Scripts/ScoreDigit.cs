using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDigit : MonoBehaviour
{
    public List<Sprite> digitSprites; // List of sprites from 0-9
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetDigit(int number)
    {
        if (number >= 0 && number <= 9)
        {
            spriteRenderer.sprite = digitSprites[number];
        }
    }
}