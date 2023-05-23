using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public void ImpossibleButton()
    {
        SceneManager.LoadSceneAsync("LoadingScreen");
        ScoreManager.enemyScore = 0;
        PlayerController.ammoCount = 60;
        PlayerController.canShoot = true;
        Time.timeScale = 1.0f;
    }

    public void DifficultSelect()
    {
        SceneManager.LoadScene("DifficultySelect");
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("DifficultySelect"); 
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
