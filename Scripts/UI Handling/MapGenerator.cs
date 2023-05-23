using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] randomMaps;
    public static bool spawnMapAgain = false;

    // Start is called before the first frame update
    void Start()
    {   
        int index = Random.Range(0, randomMaps.Length);
        Instantiate(randomMaps[index], transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        int index = Random.Range(0, randomMaps.Length);

        if (spawnMapAgain == true)
        {
            Instantiate(randomMaps[index], transform.position, Quaternion.identity);
            spawnMapAgain = false;
        }
    }
}
