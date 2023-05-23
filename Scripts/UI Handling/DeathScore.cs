using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DeathScore : MonoBehaviour
{
    public TextMeshProUGUI totalEnemyScore; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalEnemyScore.text = "Your score was " + ScoreManager.enemyScore; 
    }
}
