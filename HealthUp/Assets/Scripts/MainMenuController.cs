using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//*********************************************
//MonoBehaviour used from UnityEngine library
//*********************************************

public class MainMenuController : MonoBehaviour
{

    private readonly CursorLockMode wantedMode = CursorLockMode.None;
    private GameController control;

    public Slider healthSlider, hungerSlider, stressSlider;
    public Text healthVal, hungerVal, stressVal, highScore, focusAdvice;
    private int gamesPlayed;

    private List<String> advices;
    //set cursor mode on script load
    private void Awake()
    {
        Cursor.lockState = wantedMode;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != wantedMode);

        advices = new List<String>();
    }

    //show cursor lock mode for debugging on play test
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 25), Cursor.lockState.ToString());
    }

    //set time scale on start to make cursor click work
    private void Start()
    {
        Time.timeScale = 1f;
        control = GameController.instance;
        LoadStats();
    }

    //loapd survival level
    public void LoadSurvival()
    {
        SceneManager.LoadScene("Survival");
    }

    //load tutorial level
    public void LoadTutorial()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Tutorial");
    }

    //quit game
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    void LoadStats()
    {
        float healthAvg, hungerAvg, stressAvg, score;

        control.Load();
        if (control.gameCount == 0)
        {
            healthAvg = 0;
            hungerAvg = 0;
            stressAvg = 0;
            score = 0;
        }
        else
        {
            healthAvg = control.health / control.gameCount;
            hungerAvg = control.hunger / control.gameCount;
            stressAvg = control.stress / control.gameCount;
            score = control.currentHighest;
        }

        healthSlider.value = healthAvg;
        healthVal.text = "" + Math.Round(healthAvg);

        hungerSlider.value = hungerAvg;
        hungerVal.text = "" + Math.Round(hungerAvg);

        stressSlider.value = stressAvg;
        stressVal.text = "" + Math.Round(stressAvg);

        highScore.text = "High Score: " + score;

        DecideFocusAdvice(healthAvg, hungerAvg, stressAvg);
    }

    public void ResetStats()
    {
        control.ResetPlayer();
        LoadStats();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    private void DecideFocusAdvice(float health, float hunger, float stress)
    {
        String advice = "";
        if (health < 25)
        {
            advices.Add("Focus On:\n Remember to eat good food and avoid too many challenges");
        }
        if (hunger > 75)
        {
            advices.Add("Focus On:\n Eat regularly and stay hydrated to lower hunger");
        }
        if (stress > 60)
        {
            advices.Add("Focus On:\n Exercise and eat to lower stress");
        }
        StartCoroutine("AdviceList");
    }

    IEnumerator AdviceList()
    {
        int count = 0;
        if (advices.Count != 0)
        {
            for (; ; )
            {
                if (count == advices.Count)
                {
                    count = 0;
                }
                focusAdvice.text = advices[count];
                count++;
                yield return new WaitForSeconds(1.5f);
            }
        }
        focusAdvice.text = "Focus On:\nWell done on staying healthy!";

    }
}
