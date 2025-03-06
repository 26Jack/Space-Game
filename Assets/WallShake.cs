using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShake : MonoBehaviour
{
    public float shakeIntensity = 0.1f;
    public float shakeDuration = 0.2f; 
    public float shakeCooldown = 0.1f;

    private float timer = 0;
    private Vector3 originalPosition;
    private bool isShaking = false;

    public float detectionRadius = 10f;

    SpriteRenderer spriteRenderer;
    public float brightness = 10f;

    void Start()
    {
        originalPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player Bullet");

        foreach (GameObject target in targets)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);

            if (distance <= detectionRadius && !isShaking)
            {
                StartCoroutine(Shake());
                break;
            }
        }
    }

    IEnumerator Shake()
    {
        isShaking = true;
        float elapsed = 0f;

        Color originalColor = spriteRenderer.color;
        Color newColor = spriteRenderer.color * (1 + brightness);
        spriteRenderer.color = newColor;

        while (elapsed < shakeDuration)
        {
            float xOffset = Random.Range(-shakeIntensity, shakeIntensity);
            float yOffset = Random.Range(-shakeIntensity, shakeIntensity);
            transform.position = originalPosition + new Vector3(xOffset, yOffset, 0f);
            elapsed += Time.deltaTime;
            yield return null; // wait for the next frame
        }

        spriteRenderer.color = originalColor;

        transform.position = originalPosition;
        isShaking = false;
        timer = 0;
    }
}
