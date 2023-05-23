using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicinePickUp : MonoBehaviour
{
    public static int medicinePickedUp = 0;

    public ParticleSystem healthParticleSystem;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            medicinePickedUp = 1;
            
            
            PlayerController.currentHealth += 2;
            PowerUpSpawner.powerUpCounter--;

            Instantiate(healthParticleSystem, transform.position, Quaternion.identity);
            Destroy(gameObject);

        } else if (collision.transform.tag == "Environment")
        {
            Destroy(gameObject);
        }
    }
}
