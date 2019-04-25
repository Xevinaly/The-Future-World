using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutantFiddlePlayer : MonoBehaviour {
    private int wasPressed = 0;
    [Header("Set in Inspector")]
    public GameObject textbox;
    public int linesOfDialogue;
    
    private int linesViewed;
    private bool inRange;
    
	void FixedUpdate () {
	    if (Input.GetButtonDown("Fire1"))
	    {
	        attemptDialog();
	    }
	}

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
        linesViewed = 0;
    }
    private void OnTriggerExit(Collider other)
    {
        Text dialouge = textbox.GetComponent<Text>();
        dialouge.text = "";
        inRange = false;
        linesViewed = 0;
        wasPressed = 0;
    }

    private void attemptDialog()
    {
        if (linesViewed < linesOfDialogue && inRange)
        {
            linesViewed++;
            Text dialogue = textbox.GetComponent<Text>();
            dialogue.text = "Howdy! " + "Line: " + linesViewed;
        } else if (linesViewed >= linesOfDialogue)
        {
            Text dialogue = textbox.GetComponent<Text>();
            dialogue.text = "";
        }
    }
}
