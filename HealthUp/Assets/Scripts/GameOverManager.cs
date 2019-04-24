using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//*********************************************
//MonoBehaviour used from UnityEngine library
//*********************************************

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;       // Reference to the player's health.
    public float restartDelay = 5f;         // Time to wait before restarting the level

    public Animator exitAnimator;
    public ScoreTracker score;

    Animator anim;                          // Reference to the animator component.
    float restartTimer;                     // Timer to count up to restarting the level

    GameController control;
    public bool inTraining;

    private bool ending = false;
    private bool saving = false;


    void Awake()
    {
        // Set up the reference.
        anim = GetComponent<Animator>();
        control = GameController.instance;
        StartCoroutine("GameOverChecker");
    }


    void Update()
    {


    }


    IEnumerator GameOverChecker()
    {
        for (; ; )
        {
            // If the player has run out of health...
            if ((playerHealth.health <= 0 || playerHealth.hunger >= 100 || playerHealth.stress >= 100) && !ending)
            {
                // ... tell the animator the game is over.
                ending = true;
                exitAnimator.Play("Exit Panel In");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                score.Stop();

                if (!inTraining && !saving)
                {
                    SaveStats();
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

    void SaveStats()
    {
        saving = true;
        Debug.Log("save entered");
        control.health += playerHealth.health;
        control.hunger += playerHealth.hunger;
        control.stress += playerHealth.stress;
        control.gameCount++;
        control.CompareScores(score.GetScore());

        control.Save();
    }


}
