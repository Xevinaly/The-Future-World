using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using System;
using UnityEngine.AI;

public class PlayerControllerCylinder : MonoBehaviour
{
    Ray cameraRay;
    RaycastHit cameraRayHit;
    private bool equipped;
    private int equippedWait = 0;
    [Header("Set in Inspector")]
    public float speed = 0.5f;
    public float PlayerHealth = 100.0f;
    public int playerDamage = 10;
    Animator anim;
    public PlayerInventory inventory;
    NavMeshAgent nav;
 

    float angle = 0;
    private float camdiff;
    


    void Awake()
    {
        anim = GetComponent<Animator>();
        camdiff = Camera.main.transform.position.y - transform.position.y;
        nav = GetComponent<NavMeshAgent>();
        GetComponent<PlayerController>().SetArsenal("Empty");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && equipped)
        {
            shoot();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Rotate();
        Move();
        equippedWait++;
        if (Input.GetKeyDown("space") && equippedWait > 10)
        {
            if (equipped == false){
                equippedWait = 0;
                equipPistol();
            }
            else{
                equippedWait = 0;
                unequipWeapon();
            }
        }
    }

    void shoot()
    {
            RaycastHit hit;
            GetComponent<Actions>().Attack();
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
        if (hAxis != 0 || vAxis != 0){
            anim.SetFloat("Speed", speed);
        }
        else{
            anim.SetFloat("Speed", 0);
        }
        transform.position += pos;
        nav.updatePosition = true;

    }

    void Rotate()
    {
     
        float mouseX = Input.mousePosition.x;

        float mouseY = Input.mousePosition.y;


        Vector3 worldpos = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, camdiff));

        Vector3 lookDirection = new Vector3(worldpos.x, transform.position.y, worldpos.z);
        transform.LookAt(lookDirection);
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

    void equipPistol(){
        equipped = true;
        GetComponent<PlayerController>().SetArsenal("Gun");
        GetComponent<Actions>().Aiming();
    }
    void unequipWeapon(){
        equipped = false;
        GetComponent<Actions>().Stay();
        GetComponent<PlayerController>().SetArsenal("Empty");
    }
}
