using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolDoorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find("StandardRobot (4)") == null 
		&& GameObject.Find("StandardRobot (5)") == null 
		&& GameObject.Find("StandardRobot (6)") == null 
		&& GameObject.Find("StandardRobot (7)") == null 
		&& GameObject.Find("StandardRobot (8)") == null 
		&& GameObject.Find("door").GetComponent<Animation>().enabled == false){
			GameObject.Find("door").GetComponent<Animation>().enabled = true;
			GameObject thedoor= GameObject.Find("door");
			thedoor.GetComponent<Animation>().Play("open");
			GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = false;
		}
	}
}
