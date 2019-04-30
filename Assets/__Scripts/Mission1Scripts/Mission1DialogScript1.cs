using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using System;
using UnityEngine.UI;

public class MissionDialogScript1 : MonoBehaviour
{
 [Header("Set in Inspector")]
    public GameObject textbox;
    
    
    public void Start(){
        attemptDialog();
    }
    
    public void Update(){

    }


    private void attemptDialog()
    {
      
        Text dialogue = textbox.GetComponent<Text>();
        dialogue.text = "Howdy! ";
       
    }
}