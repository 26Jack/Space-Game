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

    public GameObject enemyBulletPrefab;

    public GameObject scoreNotif;

    private Transform target;

    public GameObject[] deathEffects;

    public float lifetime = 20;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target")?.transform;
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
        Debug.Log("applying leaving force");
        rb.AddForce(transform.up * leavingForce, ForceMode2D.Force);
    }

    public void Shoot()
    {
        if (enemyBulletPrefab != null)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, transform.rotation);
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
            GameObject notif = Instantiate(scoreNotif, transform.position, Quaternion.identity);
        }
        FindObjectOfType<ScoreManager>().AddScore(100);


        Destroy(gameObject);
    }
}