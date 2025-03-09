using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float thrust = 5f;
    public float recoil = 10f;
    public float invinThrustMod = 15;
    private Rigidbody2D rb;

    public GameObject playerBulletPrefab;
    public GameObject playerMiragePrefab;

    public float mirageTimer = 0;
    public float mirageThresh = 100;

    public float shootCooldown = 0.2f;
    private float timer = 0;

    public int ammo = 15;
    public int maxAmmo = 15;
    public float ammoTimer = 0;
    public float ammoTimerThresh = 1f;
    public float ammoReloadTime = 0.3f;

    public int maxHealth = 3;
    public int health = 3;
    public float invinTimer = 0;
    public bool invin = false;


    public GameObject[] deathEffects;
    public GameObject[] hitEffects;
    public AmmoDisplay ammoDisplay;
    public HealthDisplay healthDisplay;
    public GameObject fakeAmmoUI;
    public AudioSource audioSource;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (ammoDisplay == null)
        {
            ammoDisplay = FindObjectOfType<AmmoDisplay>();
        }

        if (healthDisplay == null)
        {
            healthDisplay = FindObjectOfType<HealthDisplay>();
        }

        ammoDisplay.UpdateAmmo(ammo);
        healthDisplay.UpdateHealth(health);

    }

    void Update()
    {
        // Rotate left and right
        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.back * rotationInput * rotationSpeed * Time.deltaTime);

        timer += Time.deltaTime;
        ammoTimer += Time.deltaTime;
        mirageTimer += Time.deltaTime;

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

        //Debug.Log(rb.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        // Thrust
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.up * thrust);
        }

        if (rb.velocity.magnitude > 80)
        {
            GameObject mirage = Instantiate(playerMiragePrefab, transform.position, transform.rotation);
            mirageTimer = 0;
            Debug.Log("mirage");
        }
    }

    public void shoot()
    {
        if (playerBulletPrefab != null)
        {
            GameObject bullet = Instantiate(playerBulletPrefab, transform.position, transform.rotation);
            LoseAmmo();
            audioSource.Play();
            ammoTimer = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy Bullet"))
        {
            if (invin == false)
            {
                Hit();
            }
        }

        if(collision.gameObject.CompareTag("Enemy Ship"))
        {
            if (invin == false)
            {
                Hit();
            }
        }
    }

    public void Hit()
    {
        health--;
        rb.velocity = Vector3.zero;

        thrust += invinThrustMod;

        hitFlashes();
        invin = true;
        healthDisplay.UpdateHealth(health);

        if (health <= 0)
        {
            Die();
        } 
        else // dont play hit effects on death
        {
            foreach (var effect in hitEffects)
            {
                // play death effects
                Instantiate(effect, transform.position, transform.rotation);
            }
        }
    }

    public void hitFlashes()
    {
        StartCoroutine(FlashPlayerSprite());
    }

    private IEnumerator FlashPlayerSprite()
    {
        int flashes = 10;
        float flashDuration = 0.1f;

        for (int i = 0; i < flashes; i++)
        {
            // toggle visibility
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(flashDuration);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(flashDuration);
        }

        invin = false;
        thrust -= invinThrustMod;
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
