using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float health = 100;
    public float deathScore;
    public bool inTraining;

    private float deathDelay;

    private void Start()
    {
        if (gameObject.tag == "Enemy")
            deathDelay = 1.1f;
        else
            deathDelay = 4.5f;
    }
    //apply melee attack damage
    void ApplyDamage(int damage)
    {
        health -= damage;
        IsDead();
    }

    //check if health reaches 0
    private void IsDead()
    {
        if (health <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        EnemyBehaviour eh = GetComponent<EnemyBehaviour>();
        eh.PlayDeath();
        if (!inTraining)
        {
            AddToScore();
            DecreaseCount();
        }

        Destroy(gameObject, deathDelay);
    }

    private void AddToScore()
    {
        GameObject score = GameObject.FindGameObjectWithTag("Score");
        ScoreTracker tracker = score.GetComponent<ScoreTracker>();
        tracker.AddEnemyScore(deathScore);
    }

    private void DecreaseCount()
    {
        GameObject enemyCounter = GameObject.FindGameObjectWithTag("Spawner");
        EnemySpawner spawner = enemyCounter.GetComponent<EnemySpawner>();
        spawner.DecrementCount();
    }
}
