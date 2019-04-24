using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//*********************************************
//MonoBehaviour used from UnityEngine library
//*********************************************

public class PlayerHealth : MonoBehaviour
{

    public float health = 100;
    public float hunger = 0;
    public float stress = 0;
    private float timer;
    public Slider healthSlider, hungerSlider, stressSlider;

    public float modifier;

    private bool damaged = false;

    // Use this for initialization
    void Start()
    {
        //health = 100;
        healthSlider.value = health;
        hungerSlider.value = hunger;
        stressSlider.value = stress;
        timer = 0;
        StartCoroutine("IncreaseStress"); //starts coroutine for stress levels
    }

    //
    private void Update()
    {
        timer += 1f * Time.deltaTime;
    }

    void FixedUpdate()
    {
        IncreaseHunger();
        //IncreaseStress();
    }

    //increase stress levels after every 2 seconds
    IEnumerator IncreaseStress()
    {
        for (; ; )
        {
            stress += 1*modifier;
            stressSlider.value = stress;
            yield return new WaitForSeconds(2);
        }
    }

    //increases hunger every frame
    void IncreaseHunger()
    {
        hunger += (1 * Time.deltaTime) * modifier;
        hungerSlider.value = hunger;
    }

    //apply general damage to player health
    //public void ApplyDamage()
    //{
    //    if (timer > 2f)
    //    {
    //        health -= 40;
    //        healthSlider.value = health;
    //        stress += 3;
    //        stressSlider.value = stress;
    //        timer = 0;
    //    }
    //}

    //applyt specific damage to player health
    public void ApplyDamage(float amount)
    {
        if (timer > 1f)
        {
            Debug.Log("applying damdgte");
            health -= (amount*modifier);
            if (health <= 0)
                health = 0;
            healthSlider.value = health;
            stress += 5;
            stressSlider.value = stress;
            timer = 0;
        }
    }

    //increases player health
    public void ApplyHealing(float amount)
    {
        health += amount;
        if (health >= 100)
            health = 100;
        healthSlider.value = health;
    }

    //simulate eating effects on hunger and stress
    public void ApplyEating(int hungerAmount, int stressAmount)
    {
        if ((hungerAmount + hunger) < 0)
        {
            hunger = 0;
        }
        else
        {
            hunger -= hungerAmount;
        }
        if (stress - stressAmount < 0)
        {
            stress = 0;
        }
        else
        {
            stress -= stressAmount;
        }
    }
}
