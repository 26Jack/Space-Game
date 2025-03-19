using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    private Rigidbody2D rb;

    public float startingForce = 100f;
    public float maxStartingForce = 100f;
    public float minStartingForce = 60f;
    public float leavingForce = 100f;
    public float slowingForce = 0.9f;
    public float delayBeforeSlow = 0.5f;
    public float delayAfterSlow = 0.1f;
    public float speedToExit = 20f;
    public float randomForce;
    public bool leaving = false;

    public int score = 150;

    public GameObject enemyBulletPrefab;

    public GameObject scoreNotif;

    private Transform target;
    private Transform soundTarget;

    public float thrustMaxLoudness = 0.2f;
    public float distance;
    public float thrustLoudness;
    public AudioClip thrustSound;
    private AudioSource thrustAudioSource;

    public AudioClip thrustSound2;
    private AudioSource thrustAudioSource2;

    public GameObject[] deathEffects;

    public float lifetime = 20;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target")?.transform;
        soundTarget = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();

        if (target != null)
        {
            // face player
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            transform.Rotate(0f, 0f, Random.Range(-15f, 15));
        }

        StartAttack();

        thrustAudioSource = gameObject.AddComponent<AudioSource>();
        thrustAudioSource.loop = false;
        thrustAudioSource.clip = thrustSound;

        thrustAudioSource2 = gameObject.AddComponent<AudioSource>();
        thrustAudioSource2.loop = false;
        thrustAudioSource2.clip = thrustSound2;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }

        if (soundTarget != null)
        {
            distance = Vector3.Distance(transform.position, soundTarget.position);
            thrustLoudness = thrustMaxLoudness - (distance / 200) * 0.3f;
            if (thrustLoudness > thrustMaxLoudness)
            {
                thrustLoudness = thrustMaxLoudness;
            }
        }

        if (!leaving)
        {
            if (!thrustAudioSource.isPlaying)
            {
                thrustAudioSource.volume = thrustLoudness;
                thrustAudioSource.Play();
            }
        }
        else
        {
            thrustAudioSource2.enabled = true;
            if (!thrustAudioSource2.isPlaying)
            {
                thrustAudioSource2.volume = thrustLoudness - 0.03f;
                thrustAudioSource2.Play();
            }
        }

    }

    public void StartAttack()
    {
        randomForce = Random.Range(minStartingForce, maxStartingForce);

        for (int i = 0; i < 100; i++)
        {
            rb.AddForce(transform.up * randomForce);
        }

        StartCoroutine(ApplySlowingForce());

    }

    IEnumerator ApplySlowingForce()
    {
        yield return new WaitForSeconds(delayBeforeSlow);

       
        while (rb.velocity.magnitude > speedToExit)
        {
            rb.velocity *= slowingForce;
            yield return new WaitForSeconds(0.05f); 
        }

        yield return new WaitForSeconds(delayAfterSlow/2);

        Shoot();

        yield return new WaitForSeconds(delayAfterSlow*3);


        RunAway();

    }

    public void RunAway()
    {
        rb.AddForce(transform.up * leavingForce, ForceMode2D.Force);
        leaving = true;
    }

    public void Shoot()
    {
        if (enemyBulletPrefab != null)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, transform.rotation);
            AudioManager.Instance.PlaySound(AudioManager.Instance.shooterShoot);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player Bullet"))
        {
            Die();
        }

        if (collision.gameObject.CompareTag("Player"))
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

        GameObject notif = Instantiate(scoreNotif, transform.position, Quaternion.identity);
        FindObjectOfType<ScoreManager>().AddScore(score);
        FindObjectOfType<KillTracker>().ShooterKilled();

        AudioManager.Instance.PlaySound(AudioManager.Instance.shooterDie);
        AudioManager.Instance.PlaySound(AudioManager.Instance.points3, 0.3f);

        Destroy(gameObject);
    }
}