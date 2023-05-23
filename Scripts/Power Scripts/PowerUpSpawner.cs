using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    // Arrays
    public Transform[] ammoSpawnerPoints;
    public GameObject[] powerUpPrefabs;

    // Timer
    private float currentTime = 0f;
    public float startingTime = 5f;
    public float stoppingTime = 0;

    // Max Count:
    public int maxNumberOfPowerUps = 3;
    public static int powerUpCounter = 0;
    private bool maxReached = false;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        SpawnPowerUpRandom();
    }

    public void SpawnPowerUpRandom()
    {
        currentTime -= 1 * Time.deltaTime;

        // When timer hits stoppingTime target, execute:
        if (currentTime <= stoppingTime)
        {
            SpawnPowerUp();
        } 

        if (powerUpCounter == maxNumberOfPowerUps)
        {
            maxReached = true;
            
        } else if (powerUpCounter < maxNumberOfPowerUps)
        {
            maxReached = false;
        }
        


    }

    public void SpawnPowerUp()
    {   
        int spawnPointIndex = Random.Range(0, ammoSpawnerPoints.Length); // grab random item from spawnerpoints
        int powerUpIndex = Random.Range(0, powerUpPrefabs.Length); // grabs random item from power ups array

        if (maxReached == false)
        {
            Instantiate(powerUpPrefabs[powerUpIndex], ammoSpawnerPoints[spawnPointIndex].position, Quaternion.identity);
            currentTime = startingTime; // reset timer
            powerUpCounter++;
        }
    }
}
