using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CutScene1 : MonoBehaviour {

	private GameObject player ;
	private int counter = 0;

	[Header("Set in Inspector")]
	public AudioSource audio;


	// Use this for initialization
	void Start () {
		player = GameObject.Find("SportyGirl");
		player.GetComponent<PlayerControllerCylinder>().enabled = false;
		GameObject.Find("door").GetComponent<Animation>().enabled = false;
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play(0);
		// while (audio.isPlaying){}

	}

	// Update is called once per frame
	void FixedUpdate () {
		counter++;
		if (counter > 900 && counter < 950){
			GameObject.Find("door").GetComponent<Animation>().enabled = true;
			player.GetComponent<Animator>().SetBool("IsWalking", true);
			Rotate();
		}
		if (counter > 950 && counter < 1100){
			GameObject.Find("door").GetComponent<Animation>().enabled = false;
			player.GetComponent<Animator>().SetBool("IsWalking", true);
			Move();
		}
		if (counter == 1100){
			GameObject.Find("SportyGirl").GetComponent<PlayerControllerCylinder>().enabled = true;
			// SceneManager.LoadSceneAsync("ProofOfConcept_Joseph", LoadSceneMode.Additive);
			SceneManager.UnloadSceneAsync("Mission1Cutscene");

		}

	}
	    void Rotate()
		{
			Quaternion currPos = player.transform.rotation;
			currPos.y += 0.1f;
			player.transform.rotation = currPos;
			
		}

	void Move()
    {
		Vector3 currPos = player.transform.position;
		currPos.z -= 0.3f;
		player.transform.position = currPos; 
    }

   
}
