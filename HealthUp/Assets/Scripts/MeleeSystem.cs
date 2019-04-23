using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSystem : MonoBehaviour
{

    public int damage = 50;
    public float distance = 10.0f;
    public float MaxDistance = 3.5f;
    public LayerMask layerMask;

    public GameObject laser;
    public Camera gameCamera;

    private Animation anim;
    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animation>();
        damage = 30;
        distance = 10.0f;

    }

    // Update is called once per frame
    //checks if raycast hit in distance and direction of enemy/food
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject laserObj = Instantiate(laser, transform.position + new Vector3(0, 3.5f, 0), gameCamera.transform.rotation);
        }
    }


}
