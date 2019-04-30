using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission1DialogScript : MonoBehaviour {

[Header("Set in Inspector")]
    public GameObject textbox;

    Text dialogue;
    bool passedSimpleSneak = false;
    bool passedComplexSneak = false;
   int dialogClicks = 0;

    
    
    public void Start(){
        dialogue = textbox.GetComponent<Text>();
        PercyInitialDialog();
    }
    
    public void Update(){
        if (transform.position.x <= 230 && !passedSimpleSneak){
            postSimpleSneakDialog();
            passedSimpleSneak = true;
            GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>().enabled = false;
            GameObject.Find("PlayerCharacter").GetComponent<Actions>().Stay();
             GameObject.Find("StandardRobot").GetComponent<Enemy>().enabled = false;

        }
        else if (Input.GetButton("Fire1") && dialogClicks == 0 && !passedComplexSneak){
            GameObject.Find("StandardRobot (1)").GetComponent<Enemy>().enabled = false;
            preComplexSneakDialog();
            dialogClicks++;
        }
        else if (Input.GetButton("Fire1") && dialogClicks == 1 && !passedComplexSneak){
            preComplexSneakDialog();
            dialogClicks++;
        }
        else if (Input.GetButton("Fire1") && dialogClicks == 2 && !passedComplexSneak){
            clearDialog();
            dialogClicks++;
            GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>().enabled = true;
            GameObject.Find("StandardRobot (1)").GetComponent<Enemy>().enabled = true;

        }

        else if (transform.position.x < 160 && !passedComplexSneak){
            postComplexSneakDialog();
            passedComplexSneak = true;
            GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>().enabled = false;
            GameObject.Find("PlayerCharacter").GetComponent<Actions>().Stay();
        }
        else if (Input.GetButton("Fire1") && dialogClicks == 3 && passedComplexSneak){
            clearDialog();
            dialogClicks++;
            GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>().enabled = true;

        }
    }


    public void PercyInitialDialog()
    {
        dialogue.text = "Percy: Let's start with something simple: sneak around the robot";
       
    }

    public void clearDialog(){
        dialogue.text = "";
       
    }

    public void postSimpleSneakDialog(){
        dialogue.text = "Katya: How could anyone fail to get past that? \nPercy: I've been suprised before.";
    }

    public void preComplexSneakDialog(){
        dialogue.text = "This is how you'll typically see this unit behave. If it spots you, it will start to follow you.  Try sneaking past it to the door on the other side.";
    }
    public void postComplexSneakDialog(){
        dialogue.text = "Very good. Next is weapons training. I’ll let the instructors take it from here.";
    }
    public void changeDialog(string text){
        dialogue.text = text;
    }

}
