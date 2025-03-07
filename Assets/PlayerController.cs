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

    public int ammo = 15;
    public int maxAmmo = 15;
    public float ammoTimer = 0;
    public float ammoTimerThresh = 1f;
    public float ammoReloadTime = 0.3f;

    public GameObject[] deathEffects;
    public AmmoDisplay ammoDisplay;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (ammoDisplay == null)
        {
            ammoDisplay = FindObjectOfType<AmmoDisplay>();
        }

        ammoDisplay.UpdateAmmo(ammo);
    }

    void Update()
    {
        // Rotate left and right
        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.back * rotationInput * rotationSpeed * Time.deltaTime);

        timer += Time.deltaTime;
        ammoTimer += Time.deltaTime;

        if (ammoTimer > ammoTimerThresh)
        {
            if (ammo < maxAmmo)
            {
                AddAmmo();
                ammoTimer -= ammoReloadTime;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (timer >= shootCooldown)
            {
                if (ammo > 0)
                {
                    shoot();
                    rb.AddForce(transform.up * -recoil);
                    timer = 0;
                }
                
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
            LoseAmmo();
            ammoTimer = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy Bullet"))
        {
            Die();
        }

        if(collision.gameObject.CompareTag("Enemy Ship"))
        {
            Die();
        }
    }

    public void Die()
    {
        foreach (var effect in deathEffects)
        {
            // play death effects
            Instantiate(effect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    public void LoseAmmo()
    {
        ammo--;
        ammoDisplay.UpdateAmmo(ammo);

    }

    public void AddAmmo()
    {
        ammo++;
        ammoDisplay.UpdateAmmo(ammo);

    }
}
