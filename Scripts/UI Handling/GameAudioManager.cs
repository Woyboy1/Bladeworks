using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    public AudioSource audioSource; 

    // Audio Clips:
    public AudioClip audioClipReload;
    public AudioClip audioClipEnemyHit;
    public AudioClip enemyGunShot;
    public AudioClip enemyBulletDisappears;
    public AudioClip mileStoneOne;
    public AudioClip nextStage;

    // Check Variables:
    public static int enemyHit;

    public static bool ammoPickedUp = false;
    public static bool enemyShot = false;
    public static bool enemyBulletDespawns = false;
    public static bool enemiesKilledMilestone = false;
    public static bool nextStageSound = false;
    

    // Medicine Sounds:
    public AudioClip medicinePickUp;
    public AudioClip ammoPickUp;

    void Update()
    {        
        // Enemy Milestone:
        if (enemiesKilledMilestone == true)
        {
            audioSource.PlayOneShot(mileStoneOne);
            enemiesKilledMilestone = false;
        }

        // Ammo Pickup sound:
        if (ammoPickedUp == true)
        {
            audioSource.PlayOneShot(audioClipReload);
            ammoPickedUp = false;
        }
        
        // Enemy Hitting Player Sound:
        if (enemyHit == 1)
        {
            audioSource.PlayOneShot(audioClipEnemyHit);
            enemyHit = 0;
        }

        // Medicine Pickup Sound:
        if (MedicinePickUp.medicinePickedUp == 1)
        {
            audioSource.PlayOneShot(medicinePickUp);
            MedicinePickUp.medicinePickedUp = 0;
        }

        // Enemy Gun shot
        if (enemyShot == true)
        {
            audioSource.PlayOneShot(enemyGunShot);
            enemyShot = false;
        }

        // Despawn Bullet
        if (enemyBulletDespawns == true)
        {
            audioSource.PlayOneShot(enemyBulletDisappears);
            enemyBulletDespawns = false;
        }

        // Next Stage Sound:
        if (nextStageSound == true)
        {
            audioSource.PlayOneShot(nextStage); 
            nextStageSound = false;
        }



    }
}
