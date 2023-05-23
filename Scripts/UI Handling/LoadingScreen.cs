using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingScreen : MonoBehaviour
{
    private Slider slider;
    public string sceneName;
    public float loadingTime = 7.25f;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value += loadingTime * Time.deltaTime; 

        if (slider.value >= 5)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
