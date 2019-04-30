using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierGauntletDemo : MonoBehaviour {
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
		GetComponent<PlayerController>().SetArsenal("Gauntlet");
		if (playerEntered){
			counter++;
			if (counter <= 35){
				actions.Walk();
				Rotate();
			}
			else if (counter <= 120){
				actions.Walk();
				Move();
			}
			else if (counter == 125){
				actions.Aiming();
				actions.Attack();
			}			
			else if (counter == 150){
				actions.Attack();
			}
			else if (counter == 175){
				Destroy(GameObject.Find("StandardRobot (20)"));
			}
			else if (counter == 225){
				Destroy(GameObject.Find("StandardRobot (16)"));
				GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>().enabled = true;

			}
			// if (counter > 30){
			// 	actions.Aiming();
			// }
			// if (counter == 40){
			// 	actions.Attack();
			// }
			// if (counter == 70){
			// 	GameObject.Find("StandardRobot (9)").GetComponent<Enemy>().timeStunned = 100;
			// 	GameObject.Find("PlayerCharacter").GetComponent<PlayerControllerCylinder>().enabled = true;
			// 	actions.Stay();
			// }
			
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
	void Move()
    {
		Vector3 currPos = transform.position;
		currPos.x -= 0.3f;
		transform.position = currPos; 
    }
}
