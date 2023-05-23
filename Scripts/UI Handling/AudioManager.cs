using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        if (instance == null)
        {
            instance = this; 
        } else
        {
            Destroy(gameObject);
        }
    }

    public void MuteButton()
    {
        Destroy(gameObject);
    }
}
