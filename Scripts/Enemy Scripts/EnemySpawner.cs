using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public float spawnRadius = 7, time = 1.5f;

    [Header("How many enemies spawn until timer is reduced:")]
    public int enemySpawned; 

    // Spawn Special Enemies:
    // coming soon;

    void Start()
    {
        
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {

    }

    public IEnumerator SpawnEnemies()
    {
        int randomEnemy = Random.Range(0, enemyPrefab.Length);
        Vector2 spawnPoint = GameObject.Find("Player").transform.position; // finding players position
        spawnPoint += Random.insideUnitCircle.normalized * spawnRadius; // adding a unit circle around player

        Instantiate(enemyPrefab[randomEnemy], spawnPoint, Quaternion.identity); // enemies will spawn in that circle

        yield return new WaitForSeconds(time);
        StartCoroutine(SpawnEnemies());
    } 
}
