using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*********************************************
//MonoBehaviour used from UnityEngine library
//*********************************************

public class FoodHealth : MonoBehaviour
{

    public int healthEffect, hungerEffect, stressEffect;
    private GameObject player;
    private PlayerHealth playerHealth;
    public Renderer colorRender;
    private AudioSource audio;
    public AudioClip pickup;

    // Use this for initialization
    //colour shader of food object
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        colorRender = GetComponent<Renderer>();
        if (healthEffect > 0)
        {
            colorRender.material.color = Color.blue;
        }
        else
        {
            colorRender.material.color = Color.red;
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(5, 30, 10) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Eat();
            audio = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioSource>();
            audio.clip = pickup;
            audio.Play();
        }

    }

    //simulate eating food effects
    void Eat()
    {
        if (healthEffect > 0)
        {
            playerHealth.ApplyHealing(healthEffect);
        }
        else
        {
            playerHealth.ApplyDamage(-1 * healthEffect);
        }
        playerHealth.ApplyEating(hungerEffect, stressEffect);
        //HideUI();
        Destroy(gameObject);
    }

}
