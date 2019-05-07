using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission2LevelDialog : MonoBehaviour {

	[Header("Set in Inspector")]
	public GameObject dialogPanel;
	public GameObject dialog;
	public GameObject door;

	private Text textBox;

	private int counter;

	private bool waitingForClick = true;
	public bool doorTriggered;

	// Use this for initialization
	void Start () {
		textBox = dialog.GetComponent<Text>();
		changeDialog("Percy: Your objective is twofold: disable the factory and obtain schematics of the newest line of Robots. Do not fail me...and come back safe.");
		this.GetComponent<PlayerControllerCylinder>().enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (!waitingForClick){
			if (counter == 0){
				clearDialog();
			this.GetComponent<PlayerControllerCylinder>().enabled = true;	
			}
			if (doorTriggered){
				if (counter == 0){
					waitingForClick = true;
					changeDialog("Admin: You know, there’s a rumor going around that these AI aren’t just self-aware. They’re actually sentient, capable of feeling things like love, regret, mercy, and -");
					this.GetComponent<PlayerControllerCylinder>().enabled = false;
					this.GetComponent<PlayerControllerCylinder>().equipped = false;
					GameObject.Find("PlayerCharacter").GetComponent<Actions>().Stay();
				}
				else if (counter == 10){
					changeDialog("Katya: Those rumors are fairytales told by civilians. AIs are just machines programmed to survive and they perceive humanity as a threat. Don't tell me you actually believe that nonsense.");
					waitingForClick = true;
				}
				else if (counter == 20){
					changeDialog("Admin: I don’t know. It's not like I've ever had the chance to have a conversation with one of them...But, out of curiosity, what would you think if the rumors are true?");
					waitingForClick = true;
				}
				else if (counter == 30){
					changeDialog("Katya: ...");
					waitingForClick = true;
				}
				else if (counter == 40){
					clearDialog();
					this.GetComponent<PlayerControllerCylinder>().enabled = true;
					door.GetComponent<Animation>().enabled = false;
				}
				counter++;

			}
		}
		else{
			if (Input.GetButton("Fire1") && counter != 81){
				waitingForClick = false;
			}		
		}
		
	}
	private void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Door1")){
			doorTriggered = true;
		}

	}


	public void changeDialog(string text){
        dialogPanel.SetActive(true);
        textBox.text = text;
    }

	public void clearDialog(){
       textBox.text = "";
       dialogPanel.SetActive(false);
    }

}
