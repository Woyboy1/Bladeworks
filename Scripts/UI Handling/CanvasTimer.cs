using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasTimer : MonoBehaviour
{
    private float currentTime;
    public float startingTime = 120;
    public float stoppingTime = 0;

    public Text timerText; 

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        
        if (currentTime <= stoppingTime)
        {
            currentTime = startingTime;
            Debug.Log("Restart counter");
            SceneManager.LoadScene("LoadingScreen2");
        }

        timerText.text = currentTime.ToString("0");
    }
}
