using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission2ConsoleDialog : MonoBehaviour {

	[Header("Set in Inspector")]
	public GameObject dialogPanel;
	public GameObject dialog;
	public GameObject player;
	public GameObject door;

	private Text textBox;

	private int counter;

	private bool triggeredConsole = false;
	private bool waitingForClick;
	private GameObject[] enemies;
	private bool disableSafeties;




	// Use this for initialization
	void Start () {
		textBox = dialog.GetComponent<Text>();
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (triggeredConsole){
			if (!waitingForClick){
				if (counter == 0){
					foreach (GameObject enemy in enemies){
						if (enemy != null){
							enemy.GetComponent<SecurityEnemy>().enabled = false;
							enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 0;
							enemy.transform.GetChild(2).gameObject.SetActive(false);
						}
					}
					player.GetComponent<PlayerControllerCylinder>().enabled = false;
					player.GetComponent<PlayerControllerCylinder>().equipped = false;
					player.GetComponent<Animator>().SetFloat("Speed",0);
					changeDialog("Admin: That console controls the Core installation systems.  You can use it to disable the factory.");
					waitingForClick = true;
				}
				else if (counter == 10){
					changeDialog("Katya: How?");
					waitingForClick = true;
				}
				else if (counter == 20){
					changeDialog("Admin: Two ways: You could disable the safeties for the installation device.  That would cause it to overwork itself until it");
					waitingForClick = true;
				}
				else if (counter == 30){
					changeDialog("Admin: ...Or you could make a small tweak to the Core interface, setting their damage threshold to zero");
					waitingForClick = true;
				}

				else if (counter == 40){
					changeDialog("Katya: What would that do?");
					waitingForClick = true;
				}
				else if (counter == 50){
					changeDialog("Admin: Trick their systems into thinking that the atmosphere itself is trying to crush them.  It'd be like suddenly finding yourself at the bottom of the ocean.  I imagine it would feel quite painful.");
					waitingForClick = true;
				}
				else if (counter == 60){
					changeDialog("Katya: Why are you telling me this? One way was enough.");
					waitingForClick = true;
				}
				else if (counter == 70){
					changeDialog("Admin: I'm curious what you'll do. Clock's ticking, Katya.  Time to make your choice.");
					waitingForClick = true;
				}
				else if (counter == 80){
					changeDialog("Press q to disable the safeties, or press e to set the damage threshold to zero.");
					waitingForClick = true;
				}
				else if (counter == 90 && disableSafeties){
					changeDialog("Admin: So, Daddy's little soldier has a heart after all.");					
					waitingForClick = true;
				}
				else if (counter == 100 && disableSafeties){
					changeDialog("Katya: Shut up and tell me where the blueprints are.  And for the record, Percy's not my father.");
					waitingForClick = true;
				}
				else if (counter == 110 && disableSafeties){
					changeDialog("Admin: Sorry. I'm hacking the plant's security now, go through the door to find the blueprints");
					waitingForClick = true;
				}
				else if (counter == 90 && !disableSafeties){
					changeDialog("Admin: That seems almost cruel");
					waitingForClick = true;
				}
				else if (counter == 100 && !disableSafeties){
					changeDialog("Katya: It was your idea.");
					waitingForClick= true;
				}
				else if (counter == 110 && !disableSafeties){
					changeDialog("Admin: That you chose to listen to. To find the blueprints, go through the door I'm about to open.");
					waitingForClick = true;
				}
				else if (counter == 120){
					clearDialog();
					foreach (GameObject enemy in enemies){
						if (enemy != null){
							enemy.GetComponent<SecurityEnemy>().enabled = true;
							enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 3;
							enemy.transform.GetChild(2).gameObject.SetActive(true);
						}
					}
					player.GetComponent<PlayerControllerCylinder>().enabled = true;
					player.GetComponent<Animator>().SetFloat("Speed",player.GetComponent<PlayerControllerCylinder>().speed);
					door.GetComponent<Animation>().Play("open");

				}
				counter++;
			}
			else{
				if (Input.GetButton("Fire1") && counter != 81){
					waitingForClick = false;
				}
				if (counter == 81 && Input.GetKeyDown(KeyCode.Q) && !Input.GetKeyDown(KeyCode.E)){
					disableSafeties = true;
					waitingForClick = false;
				}
				if (counter == 81 && !Input.GetKeyDown(KeyCode.Q) && Input.GetKeyDown(KeyCode.E)){
					waitingForClick = false;
				}
			}
		}
	}
	private void OnTriggerStay(Collider other){
		if (other.gameObject.CompareTag("Player"))
			triggeredConsole = true;
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
