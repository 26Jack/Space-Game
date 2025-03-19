using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWanderer : MonoBehaviour
{
    private Rigidbody2D rb;

    public float timer = 0;
    public float dipTime = 15f;
    public float dipForce = 150;

    public float currentForce;
    public float force = 30f;
    public float rotationSpeed = 100f;

    public float thrustMaxLoudness = 0.2f;
    public float distance;
    public float thrustLoudness;
    public AudioClip thrustSound;
    private AudioSource thrustAudioSource;

    public int score = 100;

    public int randomTarget;

    public GameObject scoreNotif;

    public Transform target;
    public Transform soundTarget;

    public GameObject[] deathEffects;

    // Start is called before the first frame update
    void Start()
    {
        randomTarget = Random.Range(1, 3);

        if(randomTarget == 1)
        {
            target = GameObject.FindGameObjectWithTag("AltTarget")?.transform;
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("AltTarget2")?.transform;
        }

        soundTarget = GameObject.FindGameObjectWithTag("Player")?.transform;

        rb = GetComponent<Rigidbody2D>();

        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.Rotate(0f, 0f, Random.Range(-15f, 15));
        }

        thrustAudioSource = gameObject.AddComponent<AudioSource>();
        thrustAudioSource.loop = false;
        thrustAudioSource.clip = thrustSound;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

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
        if (timer < dipTime)
        {
            if (target != null)
            {
                RotateTowardsTarget();
                ThurstForward();
            }
            else
            {
                Dip();
            }
        }
        else
        {
            Dip();
        }

    }
    private void RotateTowardsTarget()
    {
        float currentAngle = transform.eulerAngles.z;

        Vector2 direction = (target.position - transform.position).normalized;

        // calculate the angle to the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float angleDifference = Mathf.Abs(Mathf.DeltaAngle(currentAngle, angle));
        //Debug.Log(angleDifference);
        currentForce = force - angleDifference / 2;


        // smooth rotation
        float step = rotationSpeed * Time.fixedDeltaTime;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
    }
    private void ThurstForward()
    {
        rb.AddForce(transform.up * currentForce);
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
        FindObjectOfType<KillTracker>().WandererKilled();

        AudioManager.Instance.PlaySound(AudioManager.Instance.wandererDie);
        AudioManager.Instance.PlaySound(AudioManager.Instance.points2, 0.3f);

        Destroy(gameObject);
    }

    public void Dip()
    {
        rb.AddForce(transform.up * dipForce);
    }

}
