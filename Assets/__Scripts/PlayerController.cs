using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{

	[Header("Set in Inspector")] 
	public float speed = 0.5f;

	public PlayerInventory inventory;	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Move();
		if (Input.GetKeyDown("1") && inventory.isVisible)
		{
			if (inventory.logs.Count > 0)
			{
				Camera.main.GetComponent<AudioSource>().clip = inventory.logs[0].audioClip;
				Camera.main.GetComponent<AudioSource>().Play();
			}
		}

		if (Input.GetKeyDown("backspace"))
		{
			if (inventory.isVisible)
			{
				inventory.HideInventory();
			}
			else
			{
				inventory.showInventory();
			}
		}
	}

	void Move()
	{
		float hAxis = Input.GetAxis("Horizontal");
		float vAxis = Input.GetAxis("Vertical");
		Vector3 pos = new Vector3(-hAxis, 0, -vAxis);
		pos = pos * speed;
		pos = Quaternion.Euler(0, 45, 0) * pos;
		transform.position += pos;
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("AudioLog"))
		{
			inventory.addAudioLog(other.gameObject.GetComponent<AudioLog>());
			AudioClip logAudio = other.gameObject.GetComponent<AudioSource>().clip;
			Camera.main.GetComponent<AudioSource>().clip = logAudio;
			Camera.main.GetComponent<AudioSource>().Play();
			Destroy(other.gameObject);
		}
		else if (other.gameObject.CompareTag("Collectible"))
		{
			inventory.addCollectible(other.gameObject.GetComponent<Collectible>());
			Destroy(other.gameObject);
		}
	}
}