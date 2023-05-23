using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public ParticleSystem ammoParticleSystem;
    private SpriteRenderer spriteRenderer;
    public int ammoRewarded = 40;

    public void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.transform.tag == "Player")
        {
            GameAudioManager.ammoPickedUp = true;
            PlayerController.ammoCount += ammoRewarded;
            PlayerController.canShoot = true;


            PowerUpSpawner.powerUpCounter--;
            Instantiate(ammoParticleSystem, transform.position, Quaternion.identity);
            Destroy(gameObject);
        } else if (collision.transform.tag == "Environment")
        {
            Destroy(gameObject);
        }
    }
}
