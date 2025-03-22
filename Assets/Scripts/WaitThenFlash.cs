using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitThenFlash : MonoBehaviour
{
    private Renderer objectRenderer;
    public bool isTransparent = false;
    public Color originalColor;

    public float transparency = 0;

    public float timer = 0;
    public float waitTimer = 0;
    public float flashInterval = 1;
    public float wait = 6;

    public bool canFlash = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        waitTimer += Time.deltaTime;

        if (!canFlash)
        {
            if (waitTimer >= wait)
            {
                canFlash = true;
                waitTimer = 0;
            }
        }

        if (timer >= flashInterval)
        {
            if (canFlash)
            {
                ToggleTransparency();
                timer = 0;
            }
        }
    }

    void ToggleTransparency()
    {
        if (objectRenderer != null)
        {
            Color color = objectRenderer.material.color;
            if (isTransparent)
            {
                color.a = originalColor.a;
                objectRenderer.material.color = color;
                AudioManagerMenu.Instance.PlaySound(AudioManagerMenu.Instance.flashOff);

            }
            else
            {
                color.a = transparency; 
                objectRenderer.material.color = color;
                AudioManagerMenu.Instance.PlaySound(AudioManagerMenu.Instance.flashOn);
            }
            isTransparent = !isTransparent;
        }
    }
}