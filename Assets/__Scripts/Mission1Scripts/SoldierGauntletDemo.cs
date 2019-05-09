using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierGauntletDemo : MonoBehaviour {
	bool playerEntered = false;
	private int counter = 0;
	private Actions actions ;
	private Quaternion originalRotation;
	private Mission1DialogScript dialog;

	bool waitingForClick; 

    [Header("Set in Inspector")]
	public GameObject demoCamera;
	public SceneController sc;

	void Awake () {
		actions = GetComponent<Actions>();
		originalRotation = transform.rotation;
		dialog = GameObject.Find("PlayerCharacter").GetComponent<Mission1DialogScript>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (playerEntered && !waitingForClick){
			if (counter == 0){
				GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>().enabled = false;
				GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>().equipped = false;
				demoCamera.SetActive(true);
				dialog.changeDialog("Ranger: I'll make this quick, so pay attention.  You've been given a Dart Gauntlet like I have. You have two options with it. Option one, melee attack -");
				waitingForClick = true;
			}
			counter++;
			if (counter <= 35 && counter > 1){
				actions.Walk();
				Rotate();
			}
			else if (counter <= 120 && counter > 1){
				actions.Walk();
				Move();
			}
			else if (counter == 125){
				actions.Aiming();
				actions.Attack();
	
			}			

			else if (counter == 150){
				Destroy(GameObject.Find("StandardRobot (20)"));
				waitingForClick = true;

		}
		else if (counter == 170){
					dialog.changeDialog("Ranger: - or option two, a long-range attack");

		}

			else if (counter == 200){
				actions.Attack();
			}
			else if (counter == 250){
				Destroy(GameObject.Find("StandardRobot (16)"));
				dialog.changeDialog("Ranger: Destroy three robots with each method and you'll pass");
				waitingForClick = true;
			}
			else if (counter == 260){
								demoCamera.SetActive(false);
				dialog.changeDialog("Click to use the gauntlet's long range attack, and press 'e' to use the short range attack");
				waitingForClick = true;
			}
			else if (counter == 270){
			GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>().enabled = true;
			}
			bool flag = false;
			for (int i = 13; i < 21; i++){
				if (GameObject.Find("StandardRobot ("+ i + ")") != null){
					flag = true;
				}
			}
			if (!flag){
				dialog.changeDialog("Percy: Good work. Now to put that training to the test.  You'll be deployed at 0600 hours");
				Invoke("loadMission2", 2);
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
	void Move()
    {
		Vector3 currPos = transform.position;
		currPos.x -= 0.3f;
		transform.position = currPos; 
    }

	void loadMission2()
	{
		sc.loadMission2();
	}
}
