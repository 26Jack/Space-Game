using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 100f;
    public float spawnOffset = 0.5f;

    public float lifetime = 20;
    private float timer = 0;

    public GameObject effectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        transform.position += transform.up * spawnOffset;

        rb.AddForce(transform.up * speed);
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            PlayParticle();
            AudioManager.Instance.PlaySound(AudioManager.Instance.wallHit, 0.2f);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy Ship"))
        {
            PlayParticle();
            Destroy(gameObject);
        }
    }

    public void PlayParticle()
    {
        if (effectPrefab != null)
        {
            GameObject bulletEffect = Instantiate(effectPrefab, transform.position, transform.rotation);
        }
    }


}
