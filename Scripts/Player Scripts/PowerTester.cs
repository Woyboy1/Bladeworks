using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTester : MonoBehaviour
{
    public GameObject[] powerUpPrefabs;
    public float spawnRadius;
    public float time; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPowerUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnPowerUp()
    {
        int randomPick = Random.Range(0, powerUpPrefabs.Length);
        Vector2 spawnPoint = GameObject.Find("Player").transform.position;

        spawnPoint += Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(powerUpPrefabs[randomPick], spawnPoint, Quaternion.identity);

        yield return new WaitForSeconds(time);
        StartCoroutine(SpawnPowerUp());
    }
}
