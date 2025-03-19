using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFlash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float fadeDuration = 1f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FlashRed()
    {
        StopAllCoroutines();
        StartCoroutine(FlashAndFade());
    }

    private IEnumerator FlashAndFade()
    {
        spriteRenderer.color = Color.red;

        // fade to black
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            spriteRenderer.color = Color.Lerp(Color.red, Color.black, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // just in case
        spriteRenderer.color = Color.black;
    }
}
