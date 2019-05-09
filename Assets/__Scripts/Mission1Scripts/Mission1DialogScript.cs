    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Mission1DialogScript : MonoBehaviour {

[Header("Set in Inspector")]
    public GameObject textbox;

    Text dialogue;
    bool passedSimpleSneak = false;
    bool passedComplexSneak = false;
   int dialogClicks = 0;
   bool talking = false;
   GameObject StandardRobot1;
   Transform ShootPoint1;
    GameObject StandardRobot0;
   Transform ShootPoint0;
    int timer = 0;
    int lastClick = 0;
    GameObject panel;
    
    
    public void Start(){
        dialogue = textbox.GetComponent<Text>();
        PercyInitialDialog();
        StandardRobot1 = GameObject.Find("StandardRobot (1)");
        ShootPoint1 = StandardRobot1.transform.GetChild(4);
        StandardRobot0 = GameObject.Find("StandardRobot");
        ShootPoint0 = StandardRobot0.transform.GetChild(4);
        panel = GameObject.Find("Panel");
    }
    
    public void Update(){
        timer++;
        if (talking && StandardRobot1 != null && (ShootPoint1.GetComponent<Light>().enabled == true || ShootPoint1.GetComponent<LineRenderer>().enabled == true)){
            ShootPoint1.GetComponent<Light>().enabled = false;
            ShootPoint1.GetComponent<LineRenderer>().enabled = false;
        }
        if (talking && StandardRobot0 != null && (ShootPoint0.GetComponent<Light>().enabled == true || ShootPoint0.GetComponent<LineRenderer>().enabled == true)){
            ShootPoint0.GetComponent<Light>().enabled = false;
            ShootPoint0.GetComponent<LineRenderer>().enabled = false;
        }

        if (transform.position.x <= 260 && !passedSimpleSneak){
            postSimpleSneakDialog();
            passedSimpleSneak = true;
            GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>().enabled = false;
			GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>().equipped = false;
            GameObject.Find("PlayerCharacter").GetComponent<Actions>().Stay();

            if (StandardRobot1 != null){
                StandardRobot1.GetComponent<NavMeshAgent>().speed = 0;
                StandardRobot1.GetComponent<Enemy>().enabled = false;
                ShootPoint1.GetComponent<EnemyShooting>().DisableEffects();
                ShootPoint1.GetComponent<EnemyShooting>().enabled = false;
                ShootPoint1.GetComponent<Light>().enabled = false;
                ShootPoint1.GetComponent<LineRenderer>().enabled = false;
            }
            if (StandardRobot0 != null){
                StandardRobot0.GetComponent<NavMeshAgent>().speed = 0;
                StandardRobot0.GetComponent<Enemy>().enabled = false;
                ShootPoint0.GetComponent<EnemyShooting>().DisableEffects();
                ShootPoint0.GetComponent<EnemyShooting>().enabled = false;
                ShootPoint0.GetComponent<Light>().enabled = false;
                ShootPoint0.GetComponent<LineRenderer>().enabled = false;
                talking = true;
                lastClick = timer;
            }
        }

        else if (Input.GetButton("Fire1") && dialogClicks == 0 && !passedComplexSneak && (timer - lastClick > 10) && passedSimpleSneak){
            preComplexSneakDialog();
            dialogClicks++;
            talking = true;
            lastClick = timer;
        }

        else if (Input.GetButton("Fire1") && dialogClicks == 1 && !passedComplexSneak && (timer - lastClick > 10)){
            clearDialog();
            dialogClicks++;
            GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>().enabled = true;
            if (StandardRobot1 != null){
                StandardRobot1.GetComponent<NavMeshAgent>().speed = 4;
                StandardRobot1.GetComponent<Enemy>().enabled = true;
                ShootPoint1.GetComponent<EnemyShooting>().enabled = true;
            }
            talking = false;

        }

        else if (transform.position.x < 160 && !passedComplexSneak){
            postComplexSneakDialog();
            passedComplexSneak = true;
            GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>().enabled = false;
            GameObject.Find("PlayerCharacter").GetComponent<Actions>().Stay();
            talking = true;
            if (StandardRobot1 != null){
                StandardRobot1.GetComponent<NavMeshAgent>().speed = 0;
                StandardRobot1.GetComponent<Enemy>().enabled = false;
                ShootPoint1.GetComponent<EnemyShooting>().DisableEffects();
                ShootPoint1.GetComponent<EnemyShooting>().enabled = false;
                ShootPoint1.GetComponent<Light>().enabled = false;
                ShootPoint1.GetComponent<LineRenderer>().enabled = false;
            }
        }
        else if (Input.GetButton("Fire1") && dialogClicks == 2 && passedComplexSneak){
            clearDialog();
            dialogClicks++;
            GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>().enabled = true;
            talking = false;

        }
    }


    public void PercyInitialDialog()
    {
        dialogue.text = "Percy: Let's start with something simple: sneak around the robot";
       
    }

    public void clearDialog(){
        dialogue.text = "";
       panel.SetActive(false);
    }

    public void postSimpleSneakDialog(){
        dialogue.text = "Katya: How could anyone fail to get past that? \nPercy: I've been suprised before.";
    }

    public void preComplexSneakDialog(){
        dialogue.text = "Percy: This is how you'll typically see this unit behave. If it spots you, it will start to follow you.  Try sneaking past it to the door on the other side.";
    }
    public void postComplexSneakDialog(){
        panel.SetActive(true);
        dialogue.text = "Percy: Very good. Next is weapons training. I’ll let the instructors take it from here.";
    }
    public void changeDialog(string text){
        panel.SetActive(true);
        dialogue.text = text;
    }

}
