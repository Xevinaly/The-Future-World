
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierElectricGunDemo : MonoBehaviour {
	bool playerEntered = false;
	private int counter = 0;
	private Actions actions ;
	private Quaternion originalRotation;
	private Mission1DialogScript dialog;
	private bool waitingForClick = false;
	// Use this for initialization
	void Awake () {
		actions = GetComponent<Actions>();
		originalRotation = transform.rotation;
		dialog = GameObject.Find("PlayerCharacter").GetComponent<Mission1DialogScript>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GetComponent<PlayerController>().SetArsenal("StunGun");
		if (playerEntered && !waitingForClick){
			if (counter == 0){
				dialog.changeDialog("Electrician: When the war first started, a couple of scientists thought it would be possible to short out the machines' circuits with a jolt of electricity.  It worked for a while, but then the robots upgraded.  ");
				waitingForClick = true;
			}
			if (counter == 1){
				dialog.changeDialog("Electrician: While it's no longer a permanent way to keep 'em down, the stun gun still has some practical uses.  One blast should stun most robots for at least a few seconds.  ");
				waitingForClick = true;
			}
			counter++;
			if (counter <= 30 && counter > 2){
				actions.Walk();
				Rotate();
			}
			if (counter > 30){
				actions.Aiming();
			}
			if (counter == 40){
				actions.Attack();
			}
			if (counter == 70){
				GameObject.Find("StandardRobot (9)").GetComponent<Enemy>().timeStunned = 100;
				actions.Stay();
				dialog.changeDialog("Electrician: Try to stun the other 3 scrap heaps.  Once you do, move on to the final test.");
				waitingForClick = true;
			}
			if (counter == 80){
				dialog.changeDialog("Once you've equipped your gun, press 'q' to rotate between weapons.");
				waitingForClick = true;
			}
			if (counter == 90){
				dialog.clearDialog();
				GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>().enabled = true;
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


