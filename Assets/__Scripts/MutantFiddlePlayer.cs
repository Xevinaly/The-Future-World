using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutantFiddlePlayer : MonoBehaviour {
    private int wasPressed = 0;
    [Header("Set in Inspector")]
    public GameObject textbox, panel;
	// Use this for initialization
	void Start () {
 
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetButtonDown("Fire1") && wasPressed != 1)
        {
            wasPressed++;
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (wasPressed == 1)
        {
            wasPressed = 2;
            Text dialouge = textbox.GetComponent<Text>();
            RectTransform box = textbox.GetComponent<RectTransform>();
            dialouge.text = "Howdy!";
        } else if (wasPressed == 3)
        {
            wasPressed = 4;
            Text dialouge = textbox.GetComponent<Text>();
            dialouge.text = "This is an example of NPC dialouge!";
        } else if (wasPressed == 5)
        {
            wasPressed = 6;
            Text dialouge = textbox.GetComponent<Text>();
            dialouge.text = "Also, I'm a mutant fiddle player...";
        }
        else if (wasPressed == 7)
        {
            wasPressed = 8;
            Text dialouge = textbox.GetComponent<Text>();
            dialouge.text = "";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Text dialouge = textbox.GetComponent<Text>();
        dialouge.text = "";
        wasPressed = 0;
    }
}
