using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHandler : MonoBehaviour
{
    public static bool inTutorial, pressed;
    public GameObject HUD, displayBox;
    public GameObject[] hints;

    private GameObject activeHint;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
        //StartCoroutine("DelayTutorial");
        count = 0;
        activeHint = hints[count];
        inTutorial = true;
    }

    private void Update()
    {
        if (pressed)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                if (count == (hints.Length - 1))
                {
                    activeHint.SetActive(false);
                    displayBox.SetActive(false);
                    Time.timeScale = 1f;
                    inTutorial = false;
                    Destroy(gameObject);
                }
                else
                {
                    if (count == 2)
                    {
                        HUD.SetActive(true);
                    }
                    Debug.Log("mouse pressed count = " + count);
                    count++;
                    activeHint.SetActive(false);
                    activeHint = hints[count];
                    activeHint.SetActive(true);
                }


            }
        }
        else
        {
            WaitForButton();
        }

    }

    void WaitForButton()
    {
        if (Input.anyKeyDown)
        {
            pressed = true;
            StartCoroutine("DelayTutorial");
        }
    }

    IEnumerator DelayTutorial()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0f;
        activeHint.SetActive(true);
    }



}
