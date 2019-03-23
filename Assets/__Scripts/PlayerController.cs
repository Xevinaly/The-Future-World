using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{

	[Header("Set in Inspector")] 
	public float speed = 0.5f;

	public Material npcMaterial;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Move();
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
}
