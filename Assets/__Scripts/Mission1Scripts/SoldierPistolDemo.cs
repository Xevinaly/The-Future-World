using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPistolDemo : MonoBehaviour {
	bool playerEntered = false;
	private int counter = 0;
	private Actions actions ;
	private Quaternion originalRotation;
	// Use this for initialization
	void Awake () {
		actions = GetComponent<Actions>();
		originalRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GetComponent<PlayerController>().SetArsenal("Pistol");
		if (playerEntered){
			counter++;
			if (counter <= 30){
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
				Destroy(GameObject.Find("StandardRobot (3)"));
			}
			if (counter > 100 && counter < 110){
				Rotate();
			}
			if (counter == 120){
				actions.Attack();
				GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>().enabled = true;
				// counter = 0;
				// transform.rotation = originalRotation;
			}
			if (counter == 140){
				Destroy(GameObject.Find("StandardRobot (2)"));
				actions.Stay();
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
