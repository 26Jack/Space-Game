using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float thrust = 5f;
    public float recoil = 10f;
    private Rigidbody2D rb;

    public GameObject playerBulletPrefab;

    public float shootCooldown = 0.2f;
    private float timer = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Rotate left and right
        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.back * rotationInput * rotationSpeed * Time.deltaTime);

        timer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            if (timer >= shootCooldown)
            {
                shoot();
                rb.AddForce(transform.up * -recoil);
                timer = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        // Thrust
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.up * thrust);
        }
    }

    public void shoot()
    {
        if (playerBulletPrefab != null)
        {
            GameObject bullet = Instantiate(playerBulletPrefab, transform.position, transform.rotation);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy Bullet"))
        {
            Destroy(gameObject);
        }
    }

}
