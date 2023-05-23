using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;

    // Moving Variables:
    public float enemySpeed = 3f;
    private Transform player;
 
    // Enemy Stats Variables:
    private int hitsTillDeath = 0;
    public int _currentHealth = 3;
    public int scoreAmount = 1;
    public static int enemyCount = 0;
    public int enemyDamage = 1;

    // Particles:
    public ParticleSystem deathParticleSystem;

    // kill
    public static bool killAllEnemies = false;

    // Misc:
    private SpriteRenderer spriteRenderer;
    public EnemyHealthBar healthBar;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        healthBar.SetMaxHealth(_currentHealth);
    }

    void Update()
    {
        // Update HealthBar:
        healthBar.SetHealth(_currentHealth);

        // Rotate to player
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;


        // Enemy Death and Score Update:
        if (_currentHealth <= hitsTillDeath)
        {
            GameAudioManager.enemyHit = 1;
            ScoreManager.enemyScore += scoreAmount;
            enemyCount++;
            Destroy(gameObject);
            Instantiate(deathParticleSystem, transform.position, Quaternion.identity);
            _currentHealth = 0;
        }

        // Kill All Enemies:
        if (killAllEnemies == true)
        {
            Destroy(this.gameObject);
            Instantiate(deathParticleSystem, transform.position, Quaternion.identity);
            killAllEnemies = false;
        }

    }
    private void FixedUpdate()
    {
        // Enemy Movement:
        if (player != null)
        { 
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Hits Player:
        if (collision.transform.tag == "Player")
        {
            GameAudioManager.enemyHit = 1;
            PlayerController.currentHealth -= enemyDamage;
        } 
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Gets shot by PlayeR:
        if (collision.CompareTag("PlayerBullet"))
        {
            StartCoroutine(FlashRed());
            _currentHealth--;
        } 
        // Gets shot by Revolver:
        if (collision.CompareTag("RevolverBullet"))
        {
            StartCoroutine(FlashRed());
            _currentHealth -= 4;
        }

    }
    
    public IEnumerator FlashRed()
    {
        Color _originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = _originalColor;
    }
}
