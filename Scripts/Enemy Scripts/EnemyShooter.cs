using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    // Timer:
    private float currentTime = 0;
    public float startingTime = 3f;
    public float stoppingTime = 0;

    // Bullet Prefab:
    public GameObject bulletPrefab;
    public ParticleSystem muzzleFlashParticleSystem;
    public Transform shotPoint;

    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {

        currentTime -= 1 * Time.deltaTime;

        // Once timer hits 0, shoot:
        if (currentTime <= stoppingTime)
        {
            currentTime = startingTime;
            GameAudioManager.enemyShot = true;
            Instantiate(muzzleFlashParticleSystem, shotPoint.position, Quaternion.identity);
            Instantiate(bulletPrefab, shotPoint.transform.position, Quaternion.identity);
        }
    }

}
