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

    public AudioClip thrustSound;
    private AudioSource thrustAudioSource;
    public float thrustVolume = 0.2f;

    public AudioClip turnSound;
    private AudioSource turnAudioSource;
    public float turnVolume = 0.2f;

    public float noAmmoCooldown = 0.2f;
    private float noAmmoTimer = 0f;

    public float thrustCooldown = 0.1f;
    public float thrustTimer = 0f;

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
    public StageManager stageManager;
    public GameObject fakeAmmoUI;
    public GameObject smokeParticle;


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

        if (stageManager == null)
        {
            stageManager = FindObjectOfType<StageManager>();
        }

        thrustAudioSource = gameObject.AddComponent<AudioSource>();
        thrustAudioSource.loop = true;
        thrustAudioSource.clip = thrustSound;

        turnAudioSource = gameObject.AddComponent<AudioSource>();
        turnAudioSource.loop = true;
        turnAudioSource.clip = turnSound;

        ammoDisplay.UpdateAmmo(ammo);
        healthDisplay.UpdateHealth(health);

    }

    void Update()
    {
        // Rotate left and right
        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.back * rotationInput * rotationSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            {
                if (!turnAudioSource.isPlaying)
                {
                    turnAudioSource.volume = turnVolume;
                    turnAudioSource.Play();
                }
            }
        }
        else
        {
            if (turnAudioSource.isPlaying)
            {
                turnAudioSource.Stop();
            }
        }

        timer += Time.deltaTime;
        ammoTimer += Time.deltaTime;
        mirageTimer += Time.deltaTime;
        noAmmoTimer += Time.deltaTime;
        thrustTimer += Time.deltaTime;

        if (ammoTimer > ammoTimerThresh)
        {
            if (ammo < maxAmmo)
            {
                int randomPitch = Random.Range(1, 4);

                switch (randomPitch)
                {
                    case 1:
                        AudioManager.Instance.PlaySound(AudioManager.Instance.ammoRecharge1, 0.7f);
                        break;
                    case 2:
                        AudioManager.Instance.PlaySound(AudioManager.Instance.ammoRecharge2, 0.7f);
                        break;
                    case 3:
                        AudioManager.Instance.PlaySound(AudioManager.Instance.ammoRecharge3, 0.7f);
                        break;
                }

                AddAmmo();
                //AudioManager.Instance.PlaySound(AudioManager.Instance.ammoRecharge1);
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
                else
                {
                    if(noAmmoTimer >= noAmmoCooldown)
                    {
                        AudioManager.Instance.PlaySound(AudioManager.Instance.outOfAmmo);
                        noAmmoTimer = 0;
                    }
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

            if (!thrustAudioSource.isPlaying)
            {
                thrustAudioSource.volume = thrustVolume;
                thrustAudioSource.Play();
            }
        }
        else
        {
            if (thrustAudioSource.isPlaying)
            {
                thrustAudioSource.Stop();
            }
        }

        if (rb.velocity.magnitude > 80)
        {
            GameObject mirage = Instantiate(playerMiragePrefab, transform.position, transform.rotation);
            mirageTimer = 0;
            //Debug.Log("mirage");
        }
    }

    public void shoot()
    {
        if (playerBulletPrefab != null)
        {
            GameObject bullet = Instantiate(playerBulletPrefab, transform.position, transform.rotation);
            LoseAmmo();
            Camera.main.GetComponent<ScreenShake>().ShakeS();
            AudioManager.Instance.PlaySound(AudioManager.Instance.playerShoot, 0.8f);
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

        AudioManager.Instance.PlaySound(AudioManager.Instance.playerHit);
        FindObjectOfType<BackgroundFlash>().FlashRed();

        hitFlashes();
        invin = true;
        healthDisplay.UpdateHealth(health);

        Camera.main.GetComponent<ScreenShake>().Shake();

        if (health <= 1)
        {
            smokeParticle.SetActive(true);
        }

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
        stageManager.playerDied();
        FindObjectOfType<KillTracker>().GameOver();
        AudioManager.Instance.PlaySound(AudioManager.Instance.playerDie);

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
