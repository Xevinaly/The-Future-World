using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission1DialogScript : MonoBehaviour {

[Header("Set in Inspector")]
    public GameObject textbox;

    Text dialogue;

    
    
    public void Start(){
        dialogue = textbox.GetComponent<Text>();
        PercyInitialDialog();
    }
    
    public void Update(){
        if (this.transform.x <= 230){
            postSimpleSneakDialog();
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
        dialogue.text = "Katya: How could anyone fail to get past that? \n Percy: I've been suprised before.";
    }

    public void preComplexSneakDialog(){
        dialogue.text = "This is how you'll typically see this unit behave. If it spots you, it will start to follow you.  Try sneaking past it to the door on the other side.";
    }
    public void postComplexSneakDialog(){
        dialogue.text = "Very good. Next is weapons training. I’ll let the instructors take it from here.";
    }

}
