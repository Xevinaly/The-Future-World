using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using System;

public class PlayerControllerCylinder : MonoBehaviour
{
    Ray cameraRay;
    RaycastHit cameraRayHit;
    [Header("Set in Inspector")]
    public float speed = 0.5f;
    Animator anim;

    float angle = 0;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rotate();
        Move();
    }

    void Move()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        Vector3 pos = new Vector3(-hAxis, 0, -vAxis);
        pos = pos * speed;
        pos = (transform.rotation * Quaternion.Euler(0, 180, 0)) * pos;
        if (hAxis != 0 || vAxis != 0)
            anim.SetBool("IsWalking", true);
        else
            anim.SetBool("IsWalking", false);
        transform.position += pos;
    }

    void Rotate()
    {
        //float hAxis = Input.GetAxis("Horizontal");
        //float vAxis = Input.GetAxis("Vertical");
        //if (vAxis != 0){
        //	angle = (float) ((Math.Atan(hAxis/vAxis))*(180/Math.PI) + 45);
        //	if (vAxis > 0)
        //		angle += 180;
        //}
        //else if (hAxis < 0)
        //	angle = 135;
        //else if (hAxis > 0)
        //	angle = 315;
        //
        //transform.rotation = Quaternion.Euler(0,angle,0);

        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {
            Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
            transform.LookAt(targetPosition);
        }
    }
}
