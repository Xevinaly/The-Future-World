﻿using System.Collections;
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
    public float mouseDeadzone = 0.2f;
    Animator anim;
    public PlayerInventory inventory;

    float angle = 0;
    private float camdiff;


    void Awake()
    {
        anim = GetComponent<Animator>();
        camdiff = Camera.main.transform.position.y - transform.position.y;
    }

    private void Update()
    {

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
     
        float mouseX = Input.mousePosition.x;

        float mouseY = Input.mousePosition.y;


        Vector3 worldpos = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, camdiff));
        Vector3 lookDirection = new Vector3(worldpos.x, transform.position.y, worldpos.z);
        if ((lookDirection - transform.position).magnitude > mouseDeadzone)
        {
            transform.LookAt(lookDirection);
        }
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
