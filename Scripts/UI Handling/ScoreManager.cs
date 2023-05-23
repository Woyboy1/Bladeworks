using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private int ammoReward = 60;

    // Score Variables:
    public static int enemyScore;

    // Ammo + Guns Variables:
    public Text ammoText;
    public Text gunText;

    // Misc:
    public static bool displayNextStageText = false;
    public static bool displayDeathText = false;

    public GameObject nextStageText;
    public GameObject ammoAwardText;
    public GameObject deathText;



    void Start()
    {
        nextStageText.SetActive(false);
        ammoAwardText.SetActive(false);
        deathText.SetActive(false);
    }

    void Update()
    {
        // Display Text:
        if (displayNextStageText)
        {
            StartCoroutine(_DisplayNextStageText());
            displayNextStageText = false;
        } else if (displayDeathText)
        {
            StartCoroutine(_DisplayDeathText());
        } else if (!displayDeathText)
        {
            StopCoroutine(_DisplayDeathText());
        }


        UpdateUI();

        // Milestone One:
        if (Enemy.enemyCount == 15)
        {
            StartCoroutine(MileStoneOne());
            StartCoroutine(_DisplayAmmoAwardText());

        } else
        {
            StopCoroutine(MileStoneOne());
        }

        // Milestone Two:
        if (Enemy.enemyCount == 40)
        {
            StartCoroutine(MileStoneTwo());
            StartCoroutine(_DisplayAmmoAwardText());

        } else
        {
            StopCoroutine(MileStoneTwo());
        }

        // Next Stage Milestone:
        if (Enemy.enemyCount == 60)
        {
            StartCoroutine(NextMapMileStone());
            
        } else
        {
            StopCoroutine(NextMapMileStone());
        }

        void UpdateUI()
        {
            if (PlayerController.shotgunEquipped == true)
            {
                gunText.text = "Shotgun";
            } else if (PlayerController.pistolEquipped == true)
            {
                gunText.text = "Pistol";
            } else if (PlayerController.revolverEquipped == true)
            {
                gunText.text = "Revolver";
            }

            ammoText.text = "" +PlayerController.ammoCount;
        }

        void DestroyWithTag(string tagName)
        {
            GameObject destroyObject;
            destroyObject = GameObject.FindGameObjectWithTag(tagName);
            Destroy(destroyObject);
        }

        IEnumerator MileStoneOne()
        {
            GameAudioManager.enemiesKilledMilestone = true;
            PlayerController.ammoCount += ammoReward;
            PlayerController.mileStoneComplete = true;

            Enemy.enemyCount = 16;
            yield return null;            
        }

        IEnumerator MileStoneTwo()
        {
            GameAudioManager.enemiesKilledMilestone = true;
            PlayerController.ammoCount += ammoReward + 20;
            Enemy.enemyCount = 41;
            PlayerController.currentHealth += 5;
            yield return null;
        }

        IEnumerator NextMapMileStone()
        {
            yield return null;
            DestroyWithTag("Map");
            DestroyWithTag("Powerup");
            DestroyWithTag("Enemy");
            Enemy.enemyCount = 0;
            Debug.Log("Next Map");
            MapGenerator.spawnMapAgain = true;
            PlayerController.mileStoneMapComplete = true;
            Enemy.killAllEnemies = true;
            GameAudioManager.nextStageSound = true;
            displayNextStageText = true;

            PlayerController.ammoCount += ammoReward + 50;
            PlayerController.currentHealth += 10;
            PlayerController.canShoot = true;
            
            Time.timeScale = 0.50f;
            Time.fixedDeltaTime = Time.timeScale * .02f;
            yield return new WaitForSeconds(1f);
            Time.timeScale = 1.0f;
        }

        IEnumerator _DisplayNextStageText()
        {
            nextStageText.SetActive(true);
            yield return new WaitForSeconds(0.95f);
            nextStageText.SetActive(false);
        }

        IEnumerator _DisplayAmmoAwardText()
        {
            Time.timeScale = 0.50f;
            Time.fixedDeltaTime = Time.timeScale * .02f;

            ammoAwardText.SetActive(true);
            yield return new WaitForSeconds(0.95f);
            ammoAwardText.SetActive(false);

            Time.timeScale = 1.0f;
        }

        IEnumerator _DisplayDeathText()
        {
            yield return null;
            deathText.SetActive(true);
        }
    }
}
