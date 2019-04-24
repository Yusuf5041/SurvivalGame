using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*********************************************
//MonoBehaviour used from UnityEngine library
//*********************************************

public class TutorialEnder : MonoBehaviour
{

    public GameObject giant;
    public Animator endAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(giant == null)
        {
            endAnim.Play("Exit Panel In");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
