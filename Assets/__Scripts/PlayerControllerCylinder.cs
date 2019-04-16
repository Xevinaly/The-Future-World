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
    public float PlayerHealth = 100.0f;
    public int playerDamage = 10;
    Animator anim;

    float angle = 0;
    private float camdiff;


    void Awake()
    {
        anim = GetComponent<Animator>();
        camdiff = Camera.main.transform.position.y - transform.position.y;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Rotate();
        Move();
    }

    void shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().EnemyHealth -= playerDamage;
            }
        }
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
     
        float mouseX = Input.mousePosition.x;

        float mouseY = Input.mousePosition.y;


        Vector3 worldpos = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, camdiff));

        Vector3 lookDirection = new Vector3(worldpos.x, transform.position.y, worldpos.z);
        transform.LookAt(lookDirection);
    }
}
