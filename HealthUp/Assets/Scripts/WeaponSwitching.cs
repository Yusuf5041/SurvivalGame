using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*********************************************
//MonoBehaviour used from UnityEngine library
//*********************************************

public class WeaponSwitching : MonoBehaviour {

    public GameObject Weapon1;
    public GameObject Weapon2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            swapWeapon();
        }
	}

    void swapWeapon() {
        if (Weapon1.activeSelf)
        {
            Weapon1.SetActive(false);
            Weapon2.SetActive(true);
        }
        else {
            Weapon1.SetActive(true);
            Weapon2.SetActive(false);
        }
    }
}
