using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutantFiddlePlayer : MonoBehaviour {
    private int wasPressed = 0;
    [Header("Set in Inspector")]
    public GameObject textbox;

    private bool hasTalkedTo;
    private bool inRange;
    
	void FixedUpdate () {
		/*if (Input.GetButtonDown("Fire1") && wasPressed != 1)
        {
            wasPressed++;
        }*/
	    if (Input.GetButtonDown("Fire1"))
	    {
	        attemptDialog();
	    }
	}

    /*private void OnTriggerStay(Collider other)
    {
        if (wasPressed == 1)
        {
            wasPressed = 2;
            Text dialouge = textbox.GetComponent<Text>();
            dialouge.text = "Howdy!";
        } else if (wasPressed == 3)
        {
            wasPressed = 4;
            Text dialouge = textbox.GetComponent<Text>();
            dialouge.text = "";
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
        hasTalkedTo = false;
    }
    private void OnTriggerExit(Collider other)
    {
        Text dialouge = textbox.GetComponent<Text>();
        dialouge.text = "";
        inRange = false;
        hasTalkedTo = false;
        wasPressed = 0;
    }

    private void attemptDialog()
    {
        if (!hasTalkedTo && inRange)
        {
            Text dialogue = textbox.GetComponent<Text>();
            dialogue.text = "Howdy!";
            hasTalkedTo = true;
        } else if (hasTalkedTo)
        {
            Text dialogue = textbox.GetComponent<Text>();
            dialogue.text = "";
        }
    }
}
