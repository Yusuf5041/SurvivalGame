using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{

    private float difficulty;
    private float incrementMult = 0.5f;
    private float timer;

    private float score;

    private bool stopped;

    //public float enemyScore;

    public Text scoreDisplay, levelText;
    public EnemySpawner enemySpawn;
    public FoodSpawner foodSpawn;
    public PlayerHealth playerHealth;
    private AudioSource audio;
    public Animation difficultyAnim;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        difficulty = 1f;
        score = 100f;
        levelText.text = "x " + difficulty;
        //enemyScore = 1000f;
        if(enemySpawn != null)
        {
            StartCoroutine("DifficultyChange");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopped)
        {
            if (enemySpawn != null)
            {
                score += difficulty * Time.deltaTime;
                scoreDisplay.text = "Score: " + Math.Round(score);
            }
        }


    }

    IEnumerator DifficultyChange()
    {
        for (; ; )
        {
            difficulty += incrementMult;
            playerHealth.modifier = difficulty;

            difficultyAnim.Play();
            levelText.text = "x " + difficulty;

            enemySpawn.SetModifier(difficulty);
            enemySpawn.IncrementCount();

            foodSpawn.newWave = true;
            foodSpawn.SetModifier(difficulty);
            foodSpawn.IncrementCount();

            audio.Play();
            
            yield return new WaitForSeconds(30);
        }
    }

    public void AddEnemyScore(float enemyScore)
    {
        score += (enemyScore * difficulty);
    }

    public void Stop()
    {
        stopped = true;
    }

    public float GetScore()
    {
        return score;
    }



}
