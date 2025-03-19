using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageAdvanceFlash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private int colorIndex = 0;

    public List<Color> colors;
    public float colorSwitchInterval = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (colors.Count > 0)
        {
            spriteRenderer.color = colors[0];
            StartCoroutine(ChangeColorRoutine());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeColorRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(colorSwitchInterval);

            // next color
            colorIndex = (colorIndex + 1) % colors.Count;
            spriteRenderer.color = colors[colorIndex];
        }
    }
}
