using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrifter : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform target;
    private Transform soundTarget;

    public float startingForce = 200f;
    public float force = 12f;
    public float turnSpeed = 2f;
    public float maxTurnAngle = 10f;
    public GameObject scoreNotif;

    public float shootCooldown;
    public float shootCooldownMax = 0.8f;
    public float ShootCooldownMin = 1.3f;
    public float shootTimer = -5f;
    public int maxShots = 3;
    public int shotsFired = 0;

    public float thrustMaxLoudness = 0.2f;
    public float distance;
    public float thrustLoudness;
    public AudioClip thrustSound;
    private AudioSource thrustAudioSource;

    public GameObject enemyBulletPrefab;

    public int score = 175;

    public GameObject[] deathEffects;

    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        shootCooldown = Random.Range(shootCooldownMax, ShootCooldownMin);

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

        rb.AddForce(transform.up * startingForce);

        thrustAudioSource = gameObject.AddComponent<AudioSource>();
        thrustAudioSource.loop = false;
        thrustAudioSource.clip = thrustSound;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;


        if (shootTimer >= shootCooldown)
        {
            if(shotsFired < maxShots)
            {
                shootTimer = 0;
                shotsFired++;
                Shoot();
            }
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

        if (!thrustAudioSource.isPlaying)
        {
            thrustAudioSource.volume = thrustLoudness;
            thrustAudioSource.Play();
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.up * force);

        float randomTurnAmount = Random.Range(-maxTurnAngle, maxTurnAngle) * turnSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, 0, randomTurnAmount);
    }

    public void Shoot()
    {
        if (enemyBulletPrefab != null)
        {
            Quaternion rightRotation = transform.rotation * Quaternion.Euler(0, 0, -90); // 90° right
            Quaternion leftRotation = transform.rotation * Quaternion.Euler(0, 0, 90);  // 90° left

            // Spawn bullets
            Instantiate(enemyBulletPrefab, transform.position, rightRotation);
            Instantiate(enemyBulletPrefab, transform.position, leftRotation);
            AudioManager.Instance.PlaySound(AudioManager.Instance.drifterShoot);
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
        FindObjectOfType<KillTracker>().DrifterKilled();

        AudioManager.Instance.PlaySound(AudioManager.Instance.drifterDie);
        AudioManager.Instance.PlaySound(AudioManager.Instance.points3, 0.3f);

        Destroy(gameObject);
    }
}
