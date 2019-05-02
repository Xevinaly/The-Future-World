using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPistolDemo : MonoBehaviour {
	bool playerEntered = false;
	private int counter = 0;
	private Actions actions ;
	private Quaternion originalRotation;
	private Mission1DialogScript dialog;
	private bool waitingForClick;
	// Use this for initialization
    [Header("Set in Inspector")]

	public GameObject demoCamera;
	void Awake () {
		actions = GetComponent<Actions>();
		originalRotation = transform.rotation;
		dialog = GameObject.Find("PlayerCharacter").GetComponent<Mission1DialogScript>();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GetComponent<PlayerController>().SetArsenal("Pistol");
		if (playerEntered && !waitingForClick){
			if (counter == 0){
				demoCamera.SetActive(true);
				dialog.changeDialog("Soldier: Alright, I’m only going to say this once. Even though these things are machines, it doesn’t mean they’re invulnerable. If you shoot them enough, sooner or later, you’ll hit something important.");

			}

			counter++;

			if (counter == 5){
				waitingForClick = true;
			}
			if (counter <= 35 && counter > 5){
				actions.Walk();
				Rotate();
			}
			if (counter > 35){
				actions.Aiming();
			}
			if (counter == 45){
				actions.Attack();
				dialog.changeDialog("Soldier: The most important thing that all these machines have in common is their central core. One well placed shot there -");
			}
			if (counter == 75){
				Destroy(GameObject.Find("StandardRobot (3)"));
			}
			if (counter == 80){
				waitingForClick = true;
			}
			if (counter > 90 && counter < 100){
				Rotate();
			}
			if (counter == 110){
				actions.Attack();
				dialog.changeDialog("Soldier: -and they'll drop like flies");
			}
			if (counter == 120){
				Destroy(GameObject.Find("StandardRobot (2)"));
				actions.Stay();
			}
			if (counter == 125){
				waitingForClick = true;
			}
			if (counter == 135){
				dialog.changeDialog("Soldier: Destroy five of these things by shooting them in the core.  I want this lesson to sink in");
				demoCamera.SetActive(false);
			}

			if (counter == 140){
				waitingForClick = true;
			}
			if (counter == 150){
				dialog.changeDialog("Press the space bar to equip your pistol, and click to shoot.");
				GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>().enabled = true;
				waitingForClick = true;
			}
			if (counter == 160){
				dialog.clearDialog();
			}
		}
		else if (playerEntered && waitingForClick){
			if (Input.GetButton("Fire1")){
				waitingForClick = false;
			}
		}
	}
	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !playerEntered) 
        {
			GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>().enabled = false;
			GameObject.Find("PlayerCharacter").GetComponent<Actions>().Stay();
			playerEntered = true;
		}
	}

	void Rotate()
		{
			Quaternion currPos =transform.rotation;
			currPos.y -= 0.05f;
			transform.rotation = currPos;
		}
}
