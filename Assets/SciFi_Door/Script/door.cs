using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {
	GameObject thedoor;
	bool open;
	bool close;

void Start(){
	thedoor= GameObject.FindWithTag("SF_Door");
}
void FixedUpdate(){
	if (open && !thedoor.GetComponent<Animation>().IsPlaying("close")){
		thedoor.GetComponent<Animation>().Play("open");
		open = false;
	}
	else if (close && !thedoor.GetComponent<Animation>().IsPlaying("open")){
		thedoor.GetComponent<Animation>().Play("close");
		close = false;
	}
}
void OnTriggerEnter ( Collider obj  ){
	if (obj.gameObject.CompareTag("Player")){
		open = true;
	}
}

void OnTriggerExit ( Collider obj  ){
	if (obj.gameObject.CompareTag("Player")){
		close = true;
	}
}
}