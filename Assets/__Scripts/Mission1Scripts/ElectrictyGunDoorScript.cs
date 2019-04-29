using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrictyGunDoorScript : MonoBehaviour {
	private bool robot10Stunned = false;
	private bool robot11Stunned = false;
	private bool robot12Stunned = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (robot10Stunned && robot11Stunned && robot12Stunned  
		&& GameObject.Find("door2").GetComponent<Animation>().enabled == false){
			print("hello");
			GameObject.Find("door2").GetComponent<Animation>().enabled = true;
			GameObject thedoor= GameObject.Find("door2");
			thedoor.GetComponent<Animation>().Play("open");
			GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = false;
			Destroy(GameObject.Find("StandardRobot (9)"));
			Destroy(GameObject.Find("StandardRobot (10)"));
			Destroy(GameObject.Find("StandardRobot (11)"));
			Destroy(GameObject.Find("StandardRobot (12)"));
		}
		if (GameObject.Find("StandardRobot (10)") != null && GameObject.Find("StandardRobot (10)").GetComponent<Enemy>().timeStunned > 0){
			robot10Stunned = true;
		}
		if (GameObject.Find("StandardRobot (11)") != null && GameObject.Find("StandardRobot (11)").GetComponent<Enemy>().timeStunned > 0){
			robot11Stunned = true;
		}
		if (GameObject.Find("StandardRobot (12)") != null && GameObject.Find("StandardRobot (12)").GetComponent<Enemy>().timeStunned > 0){
			robot12Stunned = true;
		}
	}
}
