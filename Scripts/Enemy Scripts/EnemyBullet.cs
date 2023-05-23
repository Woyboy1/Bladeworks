using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bulletSpeed;
    public float bulletLifeTime;
    public int damageAmount = 1;

    // Misc:
    private Transform playerTarget;
    public ParticleSystem _bulletImpactParticleSystem;
    public ParticleSystem _bleedParticleSystem;

    Vector2 moveDirection; 

    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rb = GetComponent<Rigidbody2D>();

        moveDirection = (playerTarget.transform.position - transform.position).normalized * bulletSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);

        
    }

    void Update()
    {
        Invoke("bulletDies", bulletLifeTime);
    }

    public void bulletDies()
    {
        Destroy(gameObject);
        GameAudioManager.enemyBulletDespawns = true;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hits player:
        if (collision.CompareTag("Player"))
        {
            PlayerController.currentHealth -= damageAmount;
            Instantiate(_bulletImpactParticleSystem, transform.position, Quaternion.identity);
            GameAudioManager.enemyHit = 1;
            Destroy(gameObject);
            Instantiate(_bleedParticleSystem, transform.position, Quaternion.identity);
        }
        // Hits environment:
        else if (collision.CompareTag("Environment"))
        {
            Destroy(gameObject);
            Instantiate(_bulletImpactParticleSystem, transform.position, Quaternion.identity);
        }
    }*/ // OLD CODE!!!

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Hits player:
        if (collision.transform.tag == "Player")
        {
            PlayerController.currentHealth -= damageAmount;
            Instantiate(_bulletImpactParticleSystem, transform.position, Quaternion.identity);
            GameAudioManager.enemyHit = 1;
            Destroy(gameObject);
            Instantiate(_bleedParticleSystem, transform.position, Quaternion.identity);

        }// Hits environment:
        else if (collision.transform.tag == "Environment")
        {
            Destroy(gameObject);
            Instantiate(_bulletImpactParticleSystem, transform.position, Quaternion.identity);
            GameAudioManager.enemyBulletDespawns = true;

        } else if (collision.transform.tag == "Enemy")
        {
            Destroy(gameObject);
            Instantiate(_bulletImpactParticleSystem, transform.position, Quaternion.identity);
        }
    }
}
