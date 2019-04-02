using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    Ray cameraRay;
    RaycastHit cameraRayHit;

    [Header("Set in Inspector")] 
	public float speed = 0.5f;

	public Material npcMaterial;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Move();
        Turn();
	}

    void Turn()
    {
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {
            //if (cameraRayHit.transform.tag == "ground")
            //{
                Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                transform.LookAt(targetPosition);
            //}
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
}
