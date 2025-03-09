using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirageLogic : MonoBehaviour
{
    public float timer = 0f;
    public float lifetime;

    public Rigidbody2D playerRB;
    private SpriteRenderer spriteRenderer;

    public float playerSpeed;

    public float brightnessMod = 100;

    // Start is called before the first frame update
    void Start()
    {
        if (playerRB == null)
        {
            playerRB = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Rigidbody2D>();
        }

        if (playerRB != null)
        {
            lifetime = playerRB.velocity.magnitude / 2000;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Brightness();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
    void Brightness()
    {
        float brightness = playerRB.velocity.magnitude / brightnessMod;
        Color newColor = spriteRenderer.color * brightness;
        newColor.a = spriteRenderer.color.a;
        spriteRenderer.color = newColor;
    }
}