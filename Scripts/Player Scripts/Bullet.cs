using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Rigidbody2D rb;
    public float bulletLifeTime;
    public ParticleSystem bulletImpactParticleSystem;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Invoke("bulletDies", bulletLifeTime);
    }

    public void bulletDies()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameAudioManager.enemyHit = 1;
            Destroy(gameObject);

            // Play Particle:
            Instantiate(bulletImpactParticleSystem, transform.position, Quaternion.identity);
        }
        else if (collision.transform.tag == "Environment")
        {
            Destroy(gameObject);

            // Play Particle:
            Instantiate(bulletImpactParticleSystem, transform.position, Quaternion.identity);
        }

    }
}
